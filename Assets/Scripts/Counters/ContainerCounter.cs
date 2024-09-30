using Overcooked.Data;
using Overcooked.General;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked.Counter
{
    public class ContainerCounter : ClearCounter
    {
        [SerializeField] private InteractiveSO _interactiveObjectToCreate;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private new void Start()
        {
            base.Start();
            _spriteRenderer.sprite = _interactiveObjectToCreate.Sprite;
        }

        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (interactiveObj == null)
            {
                if (_interactiveObject == null)
                {
                    InteractiveObject newInteractiveObj = ObjectManager.Instance.InstantiateInteractiveObject(_interactiveObjectToCreate);
                    return newInteractiveObj;
                }
                else
                {
                    InteractiveObject returnedObj = _interactiveObject;
                    _interactiveObject = null;
                    return returnedObj;
                }
            }
            else
            {
                if (_interactiveObject == null)
                {
                    _interactiveObject = interactiveObj;
                    PlaceInteractiveObj(_interactiveObject);
                    return null;
                }
                else
                {
                    if (interactiveObj is IUnited)
                    {
                        if ((interactiveObj as IUnited).AddInteractiveObject(_interactiveObject.InteractiveSO))
                        {
                            ObjectManager.Instance.DestroyInteractiveObject(_interactiveObject);
                            _interactiveObject = null;
                            return interactiveObj;
                        }
                        return interactiveObj;
                    }
                    if (_interactiveObject is IUnited)
                    {
                        if ((_interactiveObject as IUnited).AddInteractiveObject(interactiveObj.InteractiveSO))
                        {
                            ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                            return null;
                        }
                        return interactiveObj;
                    }
                    return interactiveObj;

                }
            }
        }
    }
}
