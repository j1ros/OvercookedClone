using System.Collections.Generic;
using System.Linq;
using Overcooked.Data;
using Overcooked.General;
using UnityEngine;

namespace Overcooked.InteractivObject
{
    public class PlateInteractiveObject : InteractiveObject, IUnited
    {
        [SerializeField] private UnitedRecipesSO _unitedRecipes;
        [SerializeField] private Transform _placeInteractiveObj;
        private List<InteractiveSO> _unitedInteractiveObj = new List<InteractiveSO>();
        private InteractiveObject _placedInteractiveObject;
        public InteractiveObject PlacedInteractiveObject => _placedInteractiveObject;
        public List<InteractiveSO> UnitedInteractiveObj => _unitedInteractiveObj;

        private bool CanAddInteractiveObject(InteractiveSO interactiveObj, out int index)
        {
            index = 0;
            if (interactiveObj == null || _unitedInteractiveObj.Contains(interactiveObj))
                return false;

            for (int i = 0; i < _unitedRecipes.UnitedRecipe.Count; i++)
            {
                if (_unitedRecipes.UnitedRecipe[i].StartInteractiveObj.Contains(interactiveObj) && CheckUnitedInteractiveObj(i))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            ObjectManager.Instance.DestroyInteractiveObject(_placedInteractiveObject);
            _placedInteractiveObject = null;
            _unitedInteractiveObj.Clear();
        }

        public bool AddInteractiveObject(InteractiveSO interactiveObj)
        {
            if (CanAddInteractiveObject(interactiveObj, out int index))
            {
                _unitedInteractiveObj.Add(interactiveObj);
                ChangeVisual(index);
                return true;
            }
            return false;
        }

        private bool CheckUnitedInteractiveObj(int index)
        {
            IEnumerable<InteractiveSO> commonInteractiveObj = _unitedRecipes.UnitedRecipe[index].StartInteractiveObj.Intersect(_unitedInteractiveObj);
            if (commonInteractiveObj.Count() == _unitedInteractiveObj.Count)
                return true;

            return false;
        }

        private void ChangeVisual(int index)
        {
            InteractiveObject newObj = ObjectManager.Instance.InstantiateInteractiveObject(_unitedRecipes.UnitedRecipe[index].EndInteractiveObj);
            newObj.gameObject.transform.SetParent(_placeInteractiveObj, false);
            Destroy(newObj.gameObject.GetComponent<Rigidbody>());
            _placedInteractiveObject = newObj;
        }
    }
}
