using TMPro;
using UnityEngine;

namespace Overcooked.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ChangeTime(float time)
        {
            int minutes = (int)(time / 60f);
            int seconds = (int)(time % 60f);

            _text.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }
}
