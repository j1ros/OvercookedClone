using Overcooked.Data;
using Overcooked.General;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked.Counter
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private InteractiveSO _interactiveObjectToCreate;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Start()
        {
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
                    return interactiveObj;
                }
            }
        }
    }
}
