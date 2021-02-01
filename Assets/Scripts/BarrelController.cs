using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float MaxHealth = 50f;
    public float MinExplosionRadius = 2.5f;
    public float MaxExplosionRadius = 7.5f;
    public float ExplosionDamage = 20f;

    private float health;

    void Start()
    {
        health = MaxHealth;
    }

    public void Hit(float damage)
    {
        health -= damage;

        if (health <= 0) Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Random.Range(MinExplosionRadius, MaxExplosionRadius));

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<EnemyController>(out var enemy))
            {
                enemy.Shot(ExplosionDamage);
            } else if (hitCollider.TryGetComponent<PlayerManager>(out var player))
            {
                player.WasHit(ExplosionDamage / 2);
            }
        }

        Destroy(gameObject);
    }
}
