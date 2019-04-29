using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Checkpoint : MonoBehaviour {
    Animator animator;
    NavMeshAgent agent;
    bool shouldRotate = false;

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<AudioSource>()!=null)
            GetComponent<AudioSource>().Play();
        animator = other.gameObject.GetComponent<Animator>();
        agent = other.gameObject.GetComponent<NavMeshAgent>();
        if (animator != null && agent != null)
        {
            if (agent.destination.x == this.transform.position.x && agent.destination.z == this.transform.position.z)
            {
                StartCoroutine(StopAgent());
            }
        }
    }

    IEnumerator StopAgent()
    {
        shouldRotate = true;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("stopWalking", true);
        agent.isStopped = true;
    }

    void Update()
    {
        if (agent != null && shouldRotate)
        {
            Quaternion rot = Quaternion.RotateTowards(agent.gameObject.transform.rotation, this.gameObject.transform.rotation, Time.deltaTime * 50f);
            agent.gameObject.transform.rotation = rot;
            if (rot == this.gameObject.transform.rotation)
            {
                shouldRotate = false;
                GetComponentInParent<SeatManager>().onCustomerArrive();
            }
        }
    }
}
