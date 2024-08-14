using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.Data
{
    [CreateAssetMenu(menuName = "Recipe/ListRecipes")]
    public class ListRecipeSO : ScriptableObject
    {
        [SerializeField] private List<RecipeSO> _recipes;

        public List<RecipeSO> Recipes => _recipes;
    }
}
