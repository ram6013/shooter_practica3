using UnityEngine;

public class EnemyFast : Enemy
{
    protected override void Start()
    {
        base.Start();
        if (agent != null)
        {
            agent.speed = 8f;
            detectionRange = 25f;
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
    }
}
