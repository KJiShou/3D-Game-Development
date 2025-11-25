using UnityEngine;
[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 0.1f;
    public float maxDistance = 1.0f;
    public float maxSightDistance = 5.0f;
}
