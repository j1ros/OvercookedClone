using UnityEngine;
using Overcooked.UI;
using Overcooked.InteractivObject;
using Overcooked.General;
using Overcooked.Data;

namespace Overcooked.Counter
{
    public class StoveCounter : ClearCounter
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private InteractiveSO _fryingPanInteractiveSO;
        private RecipeSO _currentRecipe;
        private float _timer = 0f;
        private bool _isCooking = false;

        private new void Start()
        {
            InteractiveObject newFryingPan = ObjectManager.Instance.InstantiateInteractiveObject(_fryingPanInteractiveSO);
            newFryingPan.gameObject.transform.SetParent(_placeForInteractiveObj, false);
            _interactiveObject = newFryingPan;

            _canvas.worldCamera = Camera.main;
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.x = transform.position.x;
            _progressBar.transform.LookAt(cameraPos);
        }

        private void Update()
        {
            if (!_isCooking)
                return;

            _timer += Time.deltaTime;
            _progressBar.SetProgress(_timer / _currentRecipe.TimeToReady);
            if (_timer >= _currentRecipe.TimeToReady)
                FinishRecipe();
        }

        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (interactiveObj == null)
            {
                StopRecipe();
                InteractiveObject returnedInteractiveObj = _interactiveObject;
                _interactiveObject = null;
                return returnedInteractiveObj;
            }

            if (interactiveObj is FryingPanInteractiveObject)
            {
                if (_interactiveObject == null)
                {
                    PlaceInteractiveObj(interactiveObj);
                    StartRecipe();
                    return null;
                }

                if ((interactiveObj as FryingPanInteractiveObject).PlacedInteractiveObject != null)
                {
                    if ((_interactiveObject as FryingPanInteractiveObject).AddInteractiveObject((interactiveObj as FryingPanInteractiveObject).PlacedInteractiveObject.InteractiveSO))
                    {
                        (interactiveObj as FryingPanInteractiveObject).Clear();
                        StartRecipe();
                        return interactiveObj;
                    }
                }
            }
            else if (_interactiveObject != null)
            {
                if ((_interactiveObject as FryingPanInteractiveObject).AddInteractiveObject(interactiveObj.InteractiveSO))
                {
                    ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                    StartRecipe();
                    return null;
                }
            }

            return interactiveObj;
        }

        private void StartRecipe()
        {
            if ((_interactiveObject as FryingPanInteractiveObject)?.PlacedInteractiveObject == null)
                return;

            _currentRecipe = (_interactiveObject as FryingPanInteractiveObject).SearchFirstRecipe();
            if (_currentRecipe == null)
                return;

            _progressBar.gameObject.SetActive(true);
            _isCooking = true;
        }

        private void StopRecipe()
        {
            _timer = 0f;
            _isCooking = false;
            _progressBar.gameObject.SetActive(false);
            _currentRecipe = null;
        }

        private void FinishRecipe()
        {
            if (_currentRecipe.BurnAtTheEnd)
            {
                //-- start burning
            }
            (_interactiveObject as FryingPanInteractiveObject).Clear();
            (_interactiveObject as FryingPanInteractiveObject).AddInteractiveObject(_currentRecipe.ResultInteractiveObj);
            StopRecipe();
            StartRecipe();
        }
    }
}
