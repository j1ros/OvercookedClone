using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private CapsuleCollider _playerCollider;
        [SerializeField] private float _dashDistance;
        [SerializeField] private float _dashTime;
        private float _dashTimer;
        private bool _isDash = false;
        private Vector3 _dashStartPos;
        [HideInInspector] public bool IsWalking = false;

        private void Awake()
        {
            EventManager.StartListening(EventType.Dash, Dash);
        }

        private void Update()
        {
            DashHandle();
            MoveHandle();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Dash, Dash);
        }

        private void DashHandle()
        {
            if (!_isDash)
                return;
            if (_dashTimer > _dashTime)
            {
                _isDash = false;
                _dashTimer = 0f;
                return;
            }
            float speed = _dashDistance / _dashTime;
            float moveDistance = speed * Time.deltaTime;
            float playerRadius = _playerCollider.radius;
            float playerHeight = _playerCollider.height;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, transform.forward, moveDistance);

            if (!canMove)
            {
                _isDash = false;
                _dashTimer = 0f;
                return;
            }

            float lerpTime = _dashTimer / _dashTime;

            Vector3 lerpVector = Vector3.Lerp(_dashStartPos, _dashStartPos + (transform.forward * _dashDistance), lerpTime);
            transform.position = lerpVector;

            _dashTimer += Time.deltaTime;
        }

        private void Dash(Dictionary<EventMessageType, object> data)
        {
            if (_isDash || Time.timeScale == 0)
                return;
            _isDash = true;
            _dashStartPos = transform.position;
        }

        private void MoveHandle()
        {
            if (_gameInput == null || _isDash)
                return;

            Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            float moveDistance = _speed * Time.deltaTime;
            float playerRadius = _playerCollider.radius;
            float playerHeight = _playerCollider.height;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
                canMove = (moveDirX.x < -.5f || moveDirX.x > .5f)
                    && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = (moveDirZ.z < -.5f || moveDirZ.z > .5f)
                        && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            }

            if (canMove)
            {
                transform.position += moveDir * _speed * Time.deltaTime;
            }

            IsWalking = moveDir != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
        }
    }
}
