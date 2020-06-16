using System;
using Interaction;
using UnityEngine;

namespace Controller
{
    public class InteractionTestController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private int _speed;
        [SerializeField] private int _rotateSpeed;
        [SerializeField] private GameObject _camera;
        [SerializeField] private int _objectDistance;
        private Vector3 _playerInput;
        private float _playerRotation;
        private Transform _targetPosition;
        private bool _interact;
        private PlayerState _playerState = PlayerState.Moving;
        private Interactable _interactingObject;
        private Vector3 _offset;

        private void Awake()
        {
            _targetPosition = transform;
        }

        private void Update()
        {
            switch (_playerState)
            {
                case PlayerState.Interacting:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _playerState = PlayerState.Moving;
                    }
                    InteractMovement();
                    break;
                case PlayerState.Moving:
                    Movement();
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
            if (!_interactingObject.CanPickup) return;
            var offset = _targetPosition.position + _targetPosition.forward * _objectDistance;
            _interactingObject.transform.position =
                Vector3.MoveTowards(_interactingObject.transform.position, offset, 2);
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _targetPosition = _targetPosition == transform ? _camera.transform : transform;
            }
        }

        private void Movement()
        {
            _playerInput.x = Input.GetAxisRaw("Horizontal");
            _playerInput.z = Input.GetAxisRaw("Vertical");
            _playerRotation = Input.GetAxis("Mouse X") * _rotateSpeed;
            transform.Rotate(0, _playerRotation, 0);
            if (_playerInput == Vector3.zero) return;
            var directionVector = (_rb.transform.right * _playerInput.x) + (_rb.transform.forward * _playerInput.z);
            _rb.MovePosition(_rb.transform.position + directionVector * _speed * Time.deltaTime);
        }

        private void InteractMovement()
        {
            if (_interactingObject.CanPickup)
            {
                Interacting();
                return;
            }
            
            var offset = transform.position + transform.forward * _objectDistance;
            _interactingObject.transform.position =
                Vector3.MoveTowards(_interactingObject.transform.position, offset, 2);
            _playerInput.z = Input.GetAxisRaw("Vertical");
            if (_playerInput == Vector3.zero) return;
            
            var directionVector = _rb.transform.forward * _playerInput.z;
            if (!_interactingObject.CanPull && directionVector.z > 0 || !_interactingObject.CanPush && directionVector.z < 0) return;
            _rb.MovePosition(_rb.transform.position + directionVector * _speed * Time.deltaTime);
        }
    }
}
