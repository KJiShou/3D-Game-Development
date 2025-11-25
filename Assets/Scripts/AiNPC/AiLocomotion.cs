using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    

    NavMeshAgent agent;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (agent.hasPath)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        } else
        {
            animator.SetFloat("Speed", 0);
        }

    }
}
