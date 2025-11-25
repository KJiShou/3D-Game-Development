using UnityEngine;
using UnityEngine.AI;

public class AiPatrolState : AiState
{
    float timer = 0.0f;
    public void Enter(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }

    public void Update(AiAgent agent)
    {
        if (agent.sensor.player != null)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            if(!agent.navMeshAgent.hasPath)
            {
                WorldBounds worldBounds = GameObject.FindAnyObjectByType<WorldBounds>();
                Vector3 min = worldBounds.min.position;
                Vector3 max = worldBounds.max.position;

                Vector3 randomPosition = new Vector3(
                    Random.Range(min.x, max.x),
                    Random.Range(min.y, max.y),
                    Random.Range(min.z, max.z)
                    );
                agent.navMeshAgent.destination = randomPosition;
            }

            timer = agent.config.maxTime;
        }


        
        
    }
}
