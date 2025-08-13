using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip HitSound;
    public AudioClip FireSound;

    public AudioSource Source { get; set; }

    private void Start()
    {
        Source = EnemyManager.Instance.Source;
        Source.PlayOneShot(FireSound);
    }
    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;
        Debug.Log($"Collided with: {collidedObject.tag}");

        if (collidedObject.CompareTag("Enemy"))
        {
            Source.PlayOneShot(HitSound);
            Destroy(collidedObject);
            EnemyManager.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }
}
