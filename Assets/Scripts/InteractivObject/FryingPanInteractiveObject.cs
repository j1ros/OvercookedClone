using UnityEngine;
using Overcooked.Data;
using Overcooked.General;

namespace Overcooked.InteractivObject
{
    public class FryingPanInteractiveObject : InteractiveObject, IUnited
    {
        [SerializeField] private Transform _placeInteractiveObj;
        [SerializeField] private ListRecipeSO _possibleInteractiveObj;
        private InteractiveObject _placedInteractiveObject;
        public InteractiveObject PlacedInteractiveObject => _placedInteractiveObject;

        public bool AddInteractiveObject(InteractiveSO interactiveObj)
        {
            if (CanAddInteractiveObject(interactiveObj))
            {
                PlaceItem(interactiveObj);
                return true;
            }
            return false;
        }

        public void ClearStove()
        {
            ObjectManager.Instance.DestroyInteractiveObject(_placedInteractiveObject);
            _placedInteractiveObject = null;
        }

        public RecipeSO SearchFirstRecipe()
        {
            for (int i = 0; i < _possibleInteractiveObj.Recipes.Count; i++)
            {
                if (_possibleInteractiveObj.Recipes[i].StartInteractiveObject == _placedInteractiveObject.InteractiveSO)
                {
                    return _possibleInteractiveObj.Recipes[i];
                }
            }

            return null;
        }

        private bool CanAddInteractiveObject(InteractiveSO interactiveObj)
        {
            if (interactiveObj == null || _placedInteractiveObject != null)
                return false;
            
            for (int i = 0; i < _possibleInteractiveObj.Recipes.Count; i++)
            {
                if (_possibleInteractiveObj.Recipes[i].StartInteractiveObject == interactiveObj || _possibleInteractiveObj.Recipes[i].ResultInteractiveObj == interactiveObj)
                {
                    return true;
                }
            }
            return false;
        }

        private void PlaceItem(InteractiveSO interactiveObj)
        {
            InteractiveObject newObj = ObjectManager.Instance.InstantiateInteractiveObject(interactiveObj);
            newObj.gameObject.transform.SetParent(_placeInteractiveObj, false);
            _placedInteractiveObject = newObj;
        }
    }
}
