using UnityEngine;

namespace Actors.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        
        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            Vector3 move = new Vector3(x, 0f, z);

            transform.Translate(move * (speed * Time.deltaTime));
        }
    }
}
