using UnityEngine;
using UnityEngine.UI;

namespace Overcooked.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressFill;

        public void SetProgress(float progress)
        {
            _progressFill.fillAmount = progress;
        }

        private void OnEnable()
        {
            _progressFill.fillAmount = 0f;
        }
    }
}
