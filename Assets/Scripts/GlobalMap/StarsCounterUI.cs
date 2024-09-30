using TMPro;
using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class StarsCounterUI : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _counterText;

        private void Awake()
        {
            _counterText.text = _gameData.GetStars().ToString();
        }
    }
}
