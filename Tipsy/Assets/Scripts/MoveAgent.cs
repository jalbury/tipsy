﻿using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent agent;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetDestination();
    }

    void SetDestination()
    {
        agent.SetDestination(destination.transform.position);
    }
}
