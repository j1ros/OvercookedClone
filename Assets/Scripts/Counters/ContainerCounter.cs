using Overcooked.Data;
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
                    //-- rework need instantiate interactive obj
                    Transform newInteractiveObjTransform = Instantiate(_interactiveObjectToCreate.Prefab);
                    InteractiveObject newInteractiveObj = newInteractiveObjTransform.GetComponent<InteractiveObject>();
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
