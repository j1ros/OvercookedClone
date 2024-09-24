using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _steerSpeed;
        [SerializeField] private GameInput _gameInput;

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

            transform.Translate(Vector3.forward * _speed * moveDir.z * Time.deltaTime, Space.Self);
            if (moveDir.z != 0)
                transform.Rotate(Vector3.up, _steerSpeed * moveDir.x * Time.deltaTime);
        }
    }
}
