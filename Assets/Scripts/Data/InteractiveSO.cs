using UnityEngine;

namespace Overcooked.Data
{
    [CreateAssetMenu(menuName = "InteractiveObj/InteractiveObj")]
    public class InteractiveSO : ScriptableObject
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;

        public Transform Prefab => _prefab;
        public Sprite Sprite => _sprite;
        public string Name => _name;
    }
}
