using Overcooked.General;
using Overcooked.InteractivObject;

namespace Overcooked.Counter
{
    public class TrashCounter : BaseCounter
    {
        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (interactiveObj is IUnited)
            {
                (interactiveObj as IUnited).Clear();
                return interactiveObj;
            }
            else
            {
                ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                return null;
            }
        }
    }
}
