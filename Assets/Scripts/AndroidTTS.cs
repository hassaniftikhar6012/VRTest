using UnityEngine;

public class AndroidTTS : MonoBehaviour
{
    private AndroidJavaObject ttsObject;
    private AndroidJavaObject activityContext;
    private bool isTTSReady = false;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activityContext = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Create TTS engine with custom OnInitListener
            ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", activityContext, new TTSListener(this));
        }
#else
        isTTSReady = true; // Simulate readiness in Editor
        Debug.Log("TTS Simulated Ready in Editor");
#endif
    }

    public void Speak(string message)
    {
        if (!isTTSReady)
        {
            Debug.LogWarning("TTS not ready.");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        if (ttsObject != null)
        {
            ttsObject.Call<int>("speak", message, 1, null, null); // QUEUE_FLUSH
        }
#else
        Debug.Log("Simulated TTS in Editor: " + message);
#endif
    }

    private class TTSListener : AndroidJavaProxy
    {
        private AndroidTTS parent;

        public TTSListener(AndroidTTS tts) : base("android.speech.tts.TextToSpeech$OnInitListener")
        {
            parent = tts;
        }

        void onInit(int status)
        {
            Debug.Log("TTS onInit called with status: " + status);

#if UNITY_ANDROID && !UNITY_EDITOR
            if (status != 0)
            {
                Debug.LogWarning("Attempting to set engine manually to Google TTS...");
                try
                {
                    // Manually set Google TTS engine if initialization failed
                    parent.ttsObject.Call("setEngineByPackageName", "com.google.android.tts");
                    
                    // Re-initialize after forcing the engine
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    parent.ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", activity, new TTSListener(parent));

                    return; // Exit early; the second init will call onInit again
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to set TTS engine manually: " + e.Message);
                }
            }
#endif

            if (status == 0)
            {
                parent.isTTSReady = true;
                parent.ttsObject.Call<int>("setLanguage", new AndroidJavaObject("java.util.Locale", "en", "US"));
                Debug.Log("✅ TTS initialized successfully.");
            }
            else
            {
                Debug.LogError("❌ TTS initialization failed.");
            }
        }
    }
}
