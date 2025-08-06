using UnityEngine;
using UnityEngine.InputSystem;

public class VRGun : MonoBehaviour
{
    public GameObject BulletPrefab;       // Assign your bullet prefab here
    public Transform FirePoint;           // Position to spawn the bullet
    public float BulletSpeed = 2f;

    public GameObject BulletsParent { get; set; }

    private void Update()
    {
        // Oculus-style input using Unity's Input System
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Bullet Firing");
            FireBullet();
        }
    }

    void FireBullet()
    {
        if(BulletsParent == null)
            BulletsParent = new GameObject("Spanwed Bullets");
        
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.forward, FirePoint.rotation);
        bullet.transform.position = FirePoint.transform.position;
        bullet.transform.parent = BulletsParent.transform;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = FirePoint.forward * BulletSpeed;
        }
    }
}
