using UnityEngine;

namespace Overcooked
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interapt.performed += Interact;
            _playerInputActions.Player.Action.performed += Action;
        }

        private void OnDestroy()
        {
            _playerInputActions.Player.Interapt.performed -= Interact;
            _playerInputActions.Player.Action.performed -= Action;
            _playerInputActions.Dispose();
        }

        public Vector2 GetMovementVectorNormilized()
        {
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
            return inputVector;
        }

        private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            EventManager.TriggerEvent(EventType.Interapt, null);
        }

        private void Action(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            EventManager.TriggerEvent(EventType.Action, null);
        }
    }
}