using UnityEngine;

public class EnemyPatrol : Enemy
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    protected override void Start()
    {
        base.Start();
        
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            GameObject[] patrolObjs = GameObject.FindGameObjectsWithTag("PatrolPoint");
            if (patrolObjs.Length > 0)
            {
                patrolPoints = new Transform[patrolObjs.Length];
                for (int i = 0; i < patrolObjs.Length; i++)
                {
                    patrolPoints[i] = patrolObjs[i].transform;
                    UnityEngine.AI.NavMeshHit hit;
                    if (!UnityEngine.AI.NavMesh.SamplePosition(patrolPoints[i].position, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
                    {
                        Debug.LogWarning($"PatrolPoint {patrolObjs[i].name} is not on NavMesh!", patrolObjs[i]);
                    }
                }
                Debug.Log($"EnemyPatrol: Found {patrolPoints.Length} patrol points");
            }
            else
            {
                Debug.LogWarning("No PatrolPoint objects found in scene! Tag PatrolPoint1/2/3 as 'PatrolPoint'", gameObject);
            }
        }
    }

    protected override void UpdateBehaviour()
    {
        if (player == null || agent == null || !agent.isOnNavMesh || !agent.enabled) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0 || agent == null || !agent.isOnNavMesh || !agent.enabled) return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}
