using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Goal;
    public float Health = 50f;
    public float AttackDamage = 7.5f;
    public float RateOfAttack = 0.5f;

    private NavMeshAgent agent;
    private float cooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cooldown = 1f / (RateOfAttack * 2f);
    }

    void Update()
    {
        agent.destination = Goal.position;
    }

    public void Shot(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameTag.Player))
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                Attack(other.gameObject.GetComponent<PlayerManager>());
                cooldown = 1f / RateOfAttack;
            }
        }
    }

    private void Attack(PlayerManager player)
    {
        player.WasHit(AttackDamage);
    }
}
