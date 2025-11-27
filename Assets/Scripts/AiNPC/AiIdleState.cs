using UnityEngine;

public class AiIdleState : AiState
{
    public void Enter(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Update(AiAgent agent)
    {
        if (agent.sensor.player != null)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
