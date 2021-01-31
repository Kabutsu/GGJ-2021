using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float MovementSpeed = 30f;
    public float Lifetime = 2f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MovementSpeed;
    }
}
