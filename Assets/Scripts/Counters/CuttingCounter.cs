using Overcooked.Data;
using Overcooked.General;
using Overcooked.InteractivObject;
using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Counter
{
    public class CuttingCounter : ClearCounter
    {
        [SerializeField] private ListRecipeSO _listRecipes;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private Animator _animator;
        private RecipeSO _currentRecipe;
        private float _timer = 0f;
        private bool _isAction = false;
        private bool _isProgress = false;

        private new void Start()
        {
            _canvas.worldCamera = Camera.main;
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.x = transform.position.x;
            _progressBar.transform.LookAt(cameraPos);
        }

        private void Update()
        {
            if (!_isAction)
                return;
            //-- do anim
            _timer += Time.deltaTime;
            _progressBar.SetProgress(_timer / _currentRecipe.TimeToReady);
            if (_timer >= _currentRecipe.TimeToReady)
            {
                FinishRecipe();
                StopAction();
                _timer = 0f;
            }
        }

        private void FinishRecipe()
        {
            _isProgress = false;
            _progressBar.gameObject.SetActive(false);
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
            _progressBar.gameObject.SetActive(true);
            _isProgress = true;
            _isAction = true;
            _animator.SetBool("Cut", true);
        }

        public override void StopAction()
        {
            _isAction = false;
            _animator.SetBool("Cut", false);
        }

        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (_isProgress)
            {
                return interactiveObj;
            }
            else
            {
                return base.Interapt(interactiveObj);
            }
        }
    }
}
