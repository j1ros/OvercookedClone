using Overcooked.Data;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked.General
{
    public class ObjectManager : MonoBehaviour
    {
        public static ObjectManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        public InteractiveObject InstantiateInteractiveObject(InteractiveSO interactiveSO)
        {
            Transform newInteractiveGO = Instantiate(interactiveSO.Prefab);
            InteractiveObject newInteractiveObj = newInteractiveGO.GetComponent<InteractiveObject>();
            newInteractiveObj.SetSO(interactiveSO);
            return newInteractiveObj;
        }

        public void DestroyInteractiveObject(InteractiveObject interactiveObj)
        {
            Destroy(interactiveObj.gameObject);
        }
    }
}
