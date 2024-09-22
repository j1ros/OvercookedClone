using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Overcooked.UI
{
    public class GameEndUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _totalPoints;
        [SerializeField] private List<TextMeshProUGUI> _pointText;
        [SerializeField] private List<Image> _pointImages;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _disableColor;

        public void SetPoints(List<int> pointStars, int totalPoints)
        {
            _totalPoints.text = totalPoints.ToString();
            for (int i = 0; i < pointStars.Count; i++)
            {
                _pointText[i].text = pointStars[i].ToString();
                if (pointStars[i] <= totalPoints)
                {
                    _pointImages[i].color = _activeColor;
                }
                else
                {
                    _pointImages[i].color = _disableColor;
                }
            }
            gameObject.SetActive(true);
        }

        public void GameEnd()
        {
            //-- save lvl record 
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, "GlobalMap" } });
        }
    }
}
