using TMPro;
using UnityEngine;

namespace Overcooked.UI
{
    public class PointsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ChangePoints(int points)
        {
            _text.text = "Очки: " + points.ToString();
        }
    }
}
