using UnityEngine;
using UnityEngine.AI;

namespace Actors.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Camera mainCamera;

        private Vector3 _forward;
        private Vector3 _right;

        private void FixedUpdate()
        {
            NavMeshMovePlayer();
        }


        private void ManualMovePlayer()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            Vector3 move = new Vector3(x, 0f, z);
            transform.Translate(move * (speed * Time.deltaTime));
        }

        
        private void NavMeshMovePlayer()
        {
            var camTransform = mainCamera.transform;
            _forward = camTransform.forward;
            _right = camTransform.right;
            
            Vector3 forwardDir = new Vector3(_forward.x, 0, _forward.z).normalized;
            Vector3 rightDir = new Vector3(_right.x, 0, _right.z);
            
            // if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) agent.Move(forwardDir * (speed * Time.deltaTime));
            // if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) agent.Move(rightDir * (speed * Time.deltaTime));
            
            if(Input.GetKey(KeyCode.W)) agent.SetDestination(transform.position + forwardDir);
            if(Input.GetKey(KeyCode.A)) agent.SetDestination(transform.position - rightDir);
            if(Input.GetKey(KeyCode.S)) agent.SetDestination(transform.position - forwardDir);
            if(Input.GetKey(KeyCode.D)) agent.SetDestination(transform.position + rightDir);
        }
    }
}
