using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
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
        return AiStateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        if(!agent.enabled)
        {
            return;
        }

        if (agent.sensor.player == null)
        {
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }else
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
                timer = agent.config.maxTime;
            }
        }

            timer -= Time.deltaTime;
    }
}
