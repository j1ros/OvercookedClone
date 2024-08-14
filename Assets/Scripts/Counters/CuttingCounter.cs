using Overcooked.Data;
using Overcooked.General;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked.Counter
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private ListRecipeSO _listRecipes;
        private RecipeSO _currentRecipe;
        private float _timer = 0f;
        private bool _isAction = false;

        private void Update()
        {
            if (!_isAction)
                return;
            //-- do anim and ui
            _timer += Time.deltaTime;
            if (_timer >= _currentRecipe.TimeToReady)
            {
                _isAction = false;
                FinishRecipe();
                _timer = 0f;
            }
        }

        private void FinishRecipe()
        {
            InteractiveObject newInteractiveObj = ObjectManager.Instance.InstantiateInteractiveObject(_currentRecipe.ResultInteractiveObj);
            ObjectManager.Instance.DestroyInteractiveObject(_interactiveObject);
            PlaceInteractiveObj(newInteractiveObj);
        }

        public override bool CanAction()
        {
            for (int i = 0; i < _listRecipes.Recipes.Count; i++)
            {
                if (_listRecipes.Recipes[i].StartInteractiveObject == _interactiveObject?.InteractiveSO)
                {
                    _currentRecipe = _listRecipes.Recipes[i];
                    return true;
                }
            }
            return false;
        }

        public override void Action()
        {
            _isAction = true;
        }

        public override void StopAction()
        {
            _isAction = false;
        }
    }
}
