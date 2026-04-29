using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    public float detectionRange = 15f;
    public int contactDamage = 10;
    public float damageCooldown = 1f;

    [Header("References")]
    public Transform player;

    protected NavMeshAgent agent;
    private float lastDamageTime;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on Enemy!", gameObject);
            enabled = false;
            return;
        }
        
        agent.enabled = true;
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        UpdateBehaviour();
        CheckPlayerDamage();
    }

    protected virtual void UpdateBehaviour()
    {
        if (player == null || agent == null || !agent.isOnNavMesh || !agent.enabled) return;
        agent.SetDestination(player.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }

    void CheckPlayerDamage()
    {
        if (player == null || Time.time - lastDamageTime < damageCooldown) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 2f)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
                lastDamageTime = Time.time;
            }
        }
    }
}
