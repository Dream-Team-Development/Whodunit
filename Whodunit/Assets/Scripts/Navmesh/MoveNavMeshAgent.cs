using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NavMesh
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveNavMeshAgent : MonoBehaviour
    {
        [Header("NavMesh")]
        public Vector3 targetLocation;
        [SerializeField] private float agentSpeed = 2; // Speed should be included from character script
        private NavMeshAgent _agent;
        
        // These variables here only for testing
        // Character common rooms should be held in character personality scripts
        [Header("Character")]
        [SerializeField] private List<Room> commonRooms = new List<Room>();

        private void Start()
        {
            if(GetComponent<NavMeshAgent>()) _agent = GetComponent<NavMeshAgent>();
            _agent.speed = agentSpeed;

            targetLocation = GenerateRandomDestination();
        }

        private void Update()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                targetLocation = GenerateRandomDestination();
                MoveAgentTo(targetLocation);
            }
        }

        private void MoveAgentTo(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        private Vector3 GenerateRandomDestination()
        {
            Room targetRoom = commonRooms[Random.Range(0, commonRooms.Count)];
            Vector3 targetArea = targetRoom.transform.position;

            var xLowerBounds = targetArea.x - (targetRoom.RoomBounds.size.x / 2);
            var xUpperBounds = targetArea.x + (targetRoom.RoomBounds.size.x / 2);
            
            var zLowerBounds = targetArea.z - (targetRoom.RoomBounds.size.z / 2);
            var zUpperBounds = targetArea.z + (targetRoom.RoomBounds.size.z / 2);

            return new Vector3(Random.Range(xLowerBounds, xUpperBounds), targetLocation.y, Random.Range(zLowerBounds, zUpperBounds));
        }
    }
}
