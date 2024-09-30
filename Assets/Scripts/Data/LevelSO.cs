using System.Collections.Generic;
using Overcooked.Data;
using UnityEngine;

namespace Overcooked.Level
{
    [CreateAssetMenu(menuName = "LevelData")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _nameScene;
        [SerializeField] private float _levelTimer;
        [SerializeField] private UnitedRecipesSO _possibleRecipes;
        [SerializeField] private float _timeOnOrder;
        [SerializeField] private int _rewardForOrder;
        [SerializeField] private List<int> _pointsForStars;
        [SerializeField] private int _starForUnlock;

        public int ID => _id;
        public string NameScene => _nameScene;
        public float LevelTimer => _levelTimer;
        public UnitedRecipesSO PossibleRecipes => _possibleRecipes;
        public float TimeOnOrder => _timeOnOrder;
        public int RewardForOrder => _rewardForOrder;
        public List<int> PointsForStars => _pointsForStars;
        public int StarForUnlock => _starForUnlock;
    }
}
