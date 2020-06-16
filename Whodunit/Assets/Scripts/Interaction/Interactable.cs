using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private InteractType[] _interactTypes;
        [SerializeField] private int _weight;
        private readonly Dictionary<InteractType, int> InteractTypes = new Dictionary<InteractType, int>();
        public bool CanPush => InteractTypes.ContainsKey(InteractType.Push);
        public bool CanPull => InteractTypes.ContainsKey(InteractType.Pull);
        public bool CanPickup => InteractTypes.ContainsKey(InteractType.Pickup);

        private void Awake()
        {
            foreach (var type in _interactTypes)
            {
                InteractTypes.Add(type, _weight);
            }
        }
    }
}
