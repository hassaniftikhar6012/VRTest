using UnityEngine;

public class OrbitAroundPlayer : MonoBehaviour
{
    public Transform player;         // Assign the Player transform
    public float radius = 5f;        // Radius of orbit
    public float speed = 30f;        // Degrees per second

    private float angle = 0f;
    private Vector3 referencePosition;

    private void Start()
    {
        referencePosition = player.position;
    }
    void Update()
    {
        if (player == null) return;

        // Increase angle over time
        angle += speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        // Convert angle to radians
        float radians = angle * Mathf.Deg2Rad;

        // Calculate new position
        Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * radius;
        transform.position = referencePosition + offset;

    }
}
