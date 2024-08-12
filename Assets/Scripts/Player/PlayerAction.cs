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
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Action, Action);
        }

        private void Action(Dictionary<EventMessageType, object> message)
        {
            if (_playerInterapt.InteractiveObject != null && _playerInterapt.InteractiveObject.CanThrow)
            {
                _playerInterapt.InteractiveObject.gameObject.transform.SetParent(_parentForThrowingInteractiveObj, true);
                _playerInterapt.InteractiveObject.Throw(_playerRaycastHandle.LastInteraptVector);
                _playerInterapt.SetInteractiveObject(null);
                return;
            }

            if (_playerInterapt.SelectedCounter != null && _playerInterapt.SelectedCounter.CanAction())
            {
                // do action with counter if can
                Debug.Log("action counter");
                return;
            }
        }
    }
}
