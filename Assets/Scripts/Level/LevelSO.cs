using Overcooked.Data;
using UnityEngine;

namespace Overcooked.Level
{
    [CreateAssetMenu(menuName = "LevelData")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private float _levelTimer;
        [SerializeField] private UnitedRecipesSO _possibleRecipes;

        public float LevelTimer => _levelTimer;
        public UnitedRecipesSO PossibleRecipes => _possibleRecipes;
    }
}
