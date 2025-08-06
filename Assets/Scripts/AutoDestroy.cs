
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float LifeTime = 3f;
    
    void Start() => Destroy(gameObject, LifeTime);
}
