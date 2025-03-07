using UnityEngine;

namespace Overcooked.Data
{
    [CreateAssetMenu(menuName = "Recipe/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] private float _timeToReady;
        [SerializeField] private InteractiveSO _startInteractiveObject;
        [SerializeField] private InteractiveSO _resultInteractiveObj;
        [SerializeField] private bool _burnAtTheEnd;

        public float TimeToReady => _timeToReady;
        public InteractiveSO StartInteractiveObject => _startInteractiveObject;
        public InteractiveSO ResultInteractiveObj => _resultInteractiveObj;
        public bool BurnAtTheEnd => _burnAtTheEnd;
    }
}
