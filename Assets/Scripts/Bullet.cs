using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip HitSound;
    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;
        Debug.Log($"Collided with {collidedObject.tag}");

        if (collidedObject.CompareTag("Enemy"))
        {
            Source.PlayOneShot(HitSound);
            Destroy(collidedObject);
            EnemyManager.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }
}
