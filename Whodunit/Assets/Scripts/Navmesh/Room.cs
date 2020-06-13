using UnityEngine;

namespace NavMesh
{
    public class Room : MonoBehaviour
    {
        public Bounds RoomBounds { get; private set; }

        private void Start()
        {
            RoomBounds = CalculateSize();
        }

        private Bounds CalculateSize()
        {
            Vector3 center = Vector3.zero;

            foreach (Transform child in transform)
            {
                center += child.gameObject.GetComponent<Renderer>().bounds.center;
            }

            center /= transform.childCount;
            Bounds roomBounds = new Bounds(center, Vector3.zero);

            foreach (Transform child in transform)
            { 
                roomBounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
            }

            return roomBounds;
        }
    }
}
