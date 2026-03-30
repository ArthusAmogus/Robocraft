using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIUnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public Vector3 Destination = new(0, 0, 0);

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Destination);
    }
}
