using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.Data
{
    [CreateAssetMenu(menuName = "Recipe/UnitedRecipe")]
    public class UnitedRecipesSO : ScriptableObject
    {
        [SerializeField] private List<UnitedStruct> _unitedRecipe;

        public List<UnitedStruct> UnitedRecipe => _unitedRecipe;
    }
}
