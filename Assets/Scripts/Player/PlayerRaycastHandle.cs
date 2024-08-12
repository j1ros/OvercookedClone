using Overcooked.Counter;
using UnityEngine;
using Overcooked.InteractivObject;

namespace Overcooked.Player
{
    public class PlayerRaycastHandle : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskCounter;
        [SerializeField] private LayerMask _layerMaskInteractiveObject;
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private float _interaptDistance;
        [SerializeField] private Transform _playerInteraptTransform;
        [SerializeField] private Vector3 _halfExtents;
        [SerializeField] private PlayerInterapt _playerInterapt;
        private Vector3 _lastInteraptVector;
        public Vector3 LastInteraptVector => _lastInteraptVector;

        private void Update()
        {
            HandleInterapt();
        }

        private void HandleInterapt()
        {
            Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            bool counterIsFind = false;

            if (moveDir != Vector3.zero)
                _lastInteraptVector = moveDir;

            if (Physics.BoxCast(_playerInteraptTransform.position, _halfExtents, _lastInteraptVector, out RaycastHit raycastHitCounter, Quaternion.identity, _interaptDistance, _layerMaskCounter))
                if (raycastHitCounter.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    counterIsFind = true;
                    _playerInterapt.SetSelectedCounter(baseCounter);
                }
                else
                {
                    _playerInterapt.SetSelectedCounter(null);
                }
            else
                _playerInterapt.SetSelectedCounter(null);
            if (Physics.BoxCast(_playerInteraptTransform.position, _halfExtents, _lastInteraptVector, out RaycastHit raycastHitInteractiveObj, Quaternion.identity, _interaptDistance, _layerMaskInteractiveObject)
                && !counterIsFind)
                if (raycastHitInteractiveObj.transform.TryGetComponent(out InteractiveObject interactiveObject))
                {
                    _playerInterapt.SetSelectedInteractiveObject(interactiveObject);
                }
                else
                {
                    _playerInterapt.SetSelectedInteractiveObject(null);
                }
            else
                _playerInterapt.SetSelectedInteractiveObject(null);
        }
    }
}
