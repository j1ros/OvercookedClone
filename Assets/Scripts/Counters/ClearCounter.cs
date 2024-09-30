using UnityEngine;
using Overcooked.InteractivObject;
using Overcooked.General;
using Overcooked.Data;


namespace Overcooked.Counter
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] protected bool _startedPlate;
        [SerializeField] protected InteractiveSO _plate;
        [SerializeField] protected Transform _placeForInteractiveObj;
        protected InteractiveObject _interactiveObject;

        protected void Start()
        {
            if (_startedPlate)
            {
                InteractiveObject startedPlate = ObjectManager.Instance.InstantiateInteractiveObject(_plate);
                PlaceInteractiveObj(startedPlate);
            }
        }

        protected virtual void PlaceInteractiveObj(InteractiveObject interactiveObj)
        {
            if (interactiveObj == null)
                return;
            _interactiveObject = interactiveObj;
            interactiveObj.gameObject.transform.SetParent(_placeForInteractiveObj, false);
        }

        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (_interactiveObject == null)
            {
                PlaceInteractiveObj(interactiveObj);
                return null;
            }
            else
            {
                if (interactiveObj == null)
                {
                    InteractiveObject returnedInteractiveObj = _interactiveObject;
                    _interactiveObject = null;
                    return returnedInteractiveObj;
                }
                else if (_interactiveObject is IUnited)
                {
                    if ((_interactiveObject as IUnited).AddInteractiveObject(interactiveObj.InteractiveSO))
                    {
                        ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                        return null;
                    }

                    if ((_interactiveObject as IUnited).AddInteractiveObject((interactiveObj as IUnited)?.PlacedInteractiveObject?.InteractiveSO))
                    {
                        (interactiveObj as IUnited).Clear();
                        return interactiveObj;
                    }
                }
                else if (interactiveObj is IUnited)
                {
                    if ((interactiveObj as IUnited).AddInteractiveObject(_interactiveObject.InteractiveSO))
                    {
                        ObjectManager.Instance.DestroyInteractiveObject(_interactiveObject);
                        PlaceInteractiveObj(interactiveObj);
                        return null;
                    }
                }
            }

            return interactiveObj;
        }
    }
}
