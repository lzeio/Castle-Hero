using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Castle;
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(Castle.position);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
    }
}
