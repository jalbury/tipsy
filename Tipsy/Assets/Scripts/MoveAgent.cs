using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public Transform spawnLocation;

    public void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    public void GoToSpawnLocation()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.SetDestination(spawnLocation.position);
    }
}
