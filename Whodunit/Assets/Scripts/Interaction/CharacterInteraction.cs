using System;
using Controller;
using UnityEngine;

namespace Interaction
{
    public class CharacterInteraction : MonoBehaviour
    {
        [SerializeField] private InteractionTestController _interactionTestController;
        [SerializeField] private GameObject _camera;
        private Transform _targetPosition;
        private bool _interact;
        private PlayerState _playerState = PlayerState.Moving;
        private Interactable _interactingObject;

        private void Awake()
        {
            _targetPosition = transform;
        }

        private void Update()
        {
            switch (_playerState)
            {
                case PlayerState.Interacting:
                    Interacting();
                    break;
                case PlayerState.Moving:
                    _interact = Input.GetKeyDown(KeyCode.E);

                    if (!_interact) return;
                    if (Physics.Raycast(transform.position, transform.forward, out var hit, 1.5f))
                    {
                        var interactable = hit.collider.GetComponent<Interactable>();
                        if (interactable)
                        {
                            _interactingObject = interactable;
                            _playerState = PlayerState.Interacting;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Interacting()
        {
            if (_interactingObject.CanPickup)
            {
                _interactingObject.transform.position =
                    Vector3.MoveTowards(_interactingObject.transform.position, _targetPosition.position, 2);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    _targetPosition = _targetPosition == transform ? _camera.transform : transform;
                }
            }

            if (_interactingObject.CanPull)
            {
            }
        }
    }
}
