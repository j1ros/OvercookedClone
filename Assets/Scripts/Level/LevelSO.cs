using Overcooked.Data;
using UnityEngine;

namespace Overcooked.Level
{
    [CreateAssetMenu(menuName = "LevelData")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private float _levelTimer;
        [SerializeField] private UnitedRecipesSO _possibleRecipes;
        [SerializeField] private float _timeOnOrder;

        public float LevelTimer => _levelTimer;
        public UnitedRecipesSO PossibleRecipes => _possibleRecipes;
        public float TimeOnOrder => _timeOnOrder;
    }
}
