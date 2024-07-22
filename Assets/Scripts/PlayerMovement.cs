using UnityEngine;

namespace Overcooked
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private GameInput _gameInput;
        [HideInInspector] public bool IsWalking = false;

        private void Update()
        {
            MoveHandle();
        }

        private void MoveHandle()
        {
            if (_gameInput == null)
                return;

            Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            IsWalking = moveDir != Vector3.zero;
            transform.position += moveDir * _speed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
        }
    }
}
