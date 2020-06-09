using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace NavMesh
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveNavMeshAgent : MonoBehaviour
    {
        [Header("NavMesh")]
        public Vector3 target;
        public float agentSpeed;
        private NavMeshAgent _navMeshAgent;
        
        private void Start()
        {
            if(GetComponent<NavMeshAgent>()) _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = agentSpeed;
        }

        private void Update()
        {
            if(transform.position != target)
                MoveAgentTo(target);
        }

        private void MoveAgentTo(Vector3 location)
        {
            _navMeshAgent.SetDestination(location);
        }
    }
}
