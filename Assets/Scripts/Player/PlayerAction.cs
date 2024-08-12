using UnityEngine;
using System.Collections.Generic;

namespace Overcooked.Player
{
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private PlayerInterapt _playerInterapt;
        [SerializeField] private Transform _parentForThrowingInteractiveObj;
        [SerializeField] private PlayerRaycastHandle _playerRaycastHandle;

        private void Awake()
        {
            EventManager.StartListening(EventType.Action, Action);
            EventManager.StartListening(EventType.Abort, Abort);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Action, Action);
            EventManager.StopListening(EventType.Abort, Abort);
        }

        private void Action(Dictionary<EventMessageType, object> message)
        {
            if (_playerInterapt.InteractiveObject != null && _playerInterapt.InteractiveObject.CanThrow)
            {
                Throw(_playerRaycastHandle.LastInteraptVector);
                return;
            }

            if (_playerInterapt.SelectedCounter != null && _playerInterapt.SelectedCounter.CanAction())
            {
                // do action with counter if can
                Debug.Log("action counter");
                return;
            }
        }

        private void Abort(Dictionary<EventMessageType, object> message)
        {
            if (_playerInterapt.InteractiveObject == null)
                return;

            Throw(Vector3.zero);
            return;
        }

        private void Throw(Vector3 moveDir)
        {
            _playerInterapt.InteractiveObject.gameObject.transform.SetParent(_parentForThrowingInteractiveObj, true);
            _playerInterapt.InteractiveObject.Throw(moveDir);
            _playerInterapt.SetInteractiveObject(null);
        }
    }
}
