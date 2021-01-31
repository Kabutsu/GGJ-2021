using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnObject;
    public float MinSpawnTime = 2.5f;
    public float MaxSpawnTime = 7.5f;
    public bool IsPlayerSpawn = false;

    private float cooldown;
    private bool canCooldown;

    void Start()
    {
        ResetCooldown();
        canCooldown = !IsPlayerSpawn;
    }


    void Update()
    {
        if (canCooldown)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                Instantiate(SpawnObject, transform.position, transform.rotation);
                ResetCooldown();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameTag.IsPlayer(other.gameObject) && !IsPlayerSpawn)
        {
            canCooldown = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameTag.IsPlayer(other.gameObject) && !IsPlayerSpawn)
        {
            canCooldown = true;
        }
    }

    private void ResetCooldown()
    {
        cooldown = Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}
