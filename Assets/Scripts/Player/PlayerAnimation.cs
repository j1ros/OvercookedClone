using UnityEngine;

namespace Overcooked.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _playerAnimator.SetBool("IsWalking", _playerMovement.IsWalking);
        }
    }
}
