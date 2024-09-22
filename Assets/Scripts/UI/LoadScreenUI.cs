using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Overcooked.UI
{
    public class LoadScreenUI : MonoBehaviour
    {
        [SerializeField] private Image _loadingImage;
        [SerializeField] private TextMeshProUGUI _loadingText;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnDisable()
        {
            _loadingImage.fillAmount = 0;
            _loadingText.text = "";
        }

        public void SetProgress(float progress)
        {
            _loadingImage.fillAmount = progress;
            _loadingText.text = "LOADING: " + (int)(progress / 0.9f * 100) + "%";
        }
    }
}
