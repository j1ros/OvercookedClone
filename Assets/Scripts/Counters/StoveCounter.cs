using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overcooked.UI;

namespace Overcooked.Counter
{
    public class StoveCounter : BaseCounter
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ProgressBar _progressBar;
    }
}
