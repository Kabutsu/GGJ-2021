using UnityEngine;
using Assets.Scripts.Helpers;

public class BulletController : MonoBehaviour
{
    public float Damage = 10f;
    public float MovementSpeed = 30f;
    public float Lifetime = 2f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MovementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameTag.Enemy))
        {
            other.gameObject.GetComponent<EnemyController>().Shot(Damage);
        }

        if (!GameTag.IsPlayer(other.gameObject))
        {
            Destroy(gameObject);
        }
    }
}
