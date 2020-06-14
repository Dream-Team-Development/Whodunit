using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace NavMesh
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveNavMeshAgent : MonoBehaviour
    {
        private enum NpcState { Idle, Moving }
        
        [Header("NavMesh")]
        [SerializeField] private float agentSpeed = 2; // Speed should be included from character script
        private Vector3 _targetLocation;
        private NavMeshAgent _agent;

        // These variables here only for testing
        // Character common rooms should be held in character personality scripts
        [Header("Character")]
        [SerializeField] private List<Room> commonRooms = new List<Room>();
        private NpcState _state;

    private void Start()
        {
            if(GetComponent<NavMeshAgent>()) _agent = GetComponent<NavMeshAgent>();
            _agent.speed = agentSpeed;
            _agent.stoppingDistance = 1;

            _targetLocation = GenerateRandomDestination();
            _state = NpcState.Moving;
        }

        private void Update()
        {
            switch (_state)
            {
                case NpcState.Idle:
                    StartCoroutine(PauseInLocation());
                    break;
                
                case  NpcState.Moving:
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                        _state = NpcState.Idle;
                    break;
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

            return new Vector3(Random.Range(xLowerBounds, xUpperBounds), _targetLocation.y, Random.Range(zLowerBounds, zUpperBounds));
        }

        private IEnumerator PauseInLocation()
        {
            yield return new WaitForSeconds(Random.Range(1f, 10f));

            _targetLocation = GenerateRandomDestination();
            MoveAgentTo(_targetLocation);
            _state = NpcState.Moving;
        }
    }
}
