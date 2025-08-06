using UnityEngine;

public class TTSReader : MonoBehaviour
{
    private AndroidTTS tts;

    void Start()
    {
        tts = GetComponent<AndroidTTS>();
        InvokeRepeating(nameof(Speak), 1, 2);
    }

    void Speak()
    {
        tts.Speak("Welcome to Blind Play VR!");
        Debug.Log("Sound played");
    }
}
