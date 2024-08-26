using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.Data
{
    [Serializable]
    public struct UnitedStruct
    {
        [SerializeField] private List<InteractiveSO> _startInteractiveObj;
        [SerializeField] private InteractiveSO _endInteractiveObj;

        public List<InteractiveSO> StartInteractiveObj => _startInteractiveObj;
        public InteractiveSO EndInteractiveObj => _endInteractiveObj;
    }
}
