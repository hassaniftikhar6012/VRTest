using UnityEngine;
using UnityEngine.EventSystems;

public class TextReader : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip voiceClip;
    public AudioSource Source;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (voiceClip != null && Source != null && !Source.isPlaying)
        {
            Source.PlayOneShot(voiceClip);
            TriggerHaptic();
        }
    }
    void TriggerHaptic()
    {
        // Uses right controller vibration
        OVRInput.SetControllerVibration(1.0f, 0.5f, OVRInput.Controller.RTouch);

        // Stop after short duration
        Invoke(nameof(StopHaptic), 0.1f);
    }

    void StopHaptic()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
