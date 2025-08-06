using UnityEngine;
using UnityEngine.InputSystem;
using Meta.XR;


public class ControllerRaycastHaptic : MonoBehaviour
{
    public float rayLength = 10f;
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.gameObject.transform.position = transform.position;
    }
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * rayLength);
        }

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayLength))
        {
            var collidedObject = hit.transform.gameObject;
            Debug.Log($"Raycast Hit with: {collidedObject.name}");

            if (!collidedObject.CompareTag("Enemy"))
                return;

            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            TriggerHaptic();
        }
        else
        {
            Debug.DrawRay(origin, direction * rayLength, Color.green);
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
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
