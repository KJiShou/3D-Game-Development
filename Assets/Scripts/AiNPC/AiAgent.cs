using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class AiAgent : MonoBehaviour
{
    [HideInInspector] public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public AiAgentConfig config;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public AiSensor sensor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AiSensor>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
