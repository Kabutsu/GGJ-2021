using UnityEngine;
using Assets.Scripts.Helpers;

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
        if (IsPlayerSpawn)
        {
            Instantiate(SpawnObject, transform.position, transform.rotation);
        } else
        {
            ResetCooldown();
        }

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
