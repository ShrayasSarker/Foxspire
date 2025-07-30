using UnityEngine;
using Pathfinding; // Important! This is required to use AIPath & AIDestinationSetter

public class EnemyAIController : MonoBehaviour
{
    void Start()
    {
        // Disable the pathfinding scripts at start
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
    }
}