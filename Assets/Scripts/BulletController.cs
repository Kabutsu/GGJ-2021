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
        if (GameTag.IsWorldItem(other.gameObject))
        {
            if (other.gameObject.TryGetComponent<EnemyController>(out var enemy))
            {
                enemy.Shot(Damage);
            } else if (other.gameObject.TryGetComponent<BarrelController>(out var barrel))
            {
                barrel.Hit(Damage);
            }

            Destroy(gameObject);
        }
    }
}
