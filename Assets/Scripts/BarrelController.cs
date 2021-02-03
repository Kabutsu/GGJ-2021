using System.Collections;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float MaxHealth = 50f;
    public float MinExplosionRadius = 2.5f;
    public float MaxExplosionRadius = 7.5f;
    public float ExplosionDamage = 20f;
    public float ExplosionTime = 1.5f;

    private float health;
    private float maxIntensity = 50f;

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
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        float explosionRadius = Random.Range(MinExplosionRadius, MaxExplosionRadius) + 1f;

        var explosionLight = gameObject.AddComponent<Light>();
        explosionLight.color = new Color(1f, 0.6f, 0.39f);
        explosionLight.range = explosionRadius;
        explosionLight.intensity = 0f;

        StartCoroutine(LightIntensity(explosionLight));

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

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

        Destroy(gameObject, ExplosionTime);
    }

    public IEnumerator LightIntensity(Light explosionLight)
    {
        float oneThird = ExplosionTime / 3f;
        float twoThirds = 2f * oneThird;

        for (float i = 0f; i <= oneThird; i += Time.deltaTime)
        {
            explosionLight.intensity = i * 2 * maxIntensity;
            yield return null;
        }

        for (float i = twoThirds; i >= 0f; i -= Time.deltaTime)
        {
            explosionLight.intensity = i * maxIntensity;
            yield return null;
        }
    }
}
