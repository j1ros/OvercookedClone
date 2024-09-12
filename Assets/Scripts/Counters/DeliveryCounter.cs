using System.Collections.Generic;
using Overcooked.General;
using Overcooked.InteractivObject;

namespace Overcooked.Counter
{
    public class DeliveryCounter : BaseCounter
    {
        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (interactiveObj is PlateInteractiveObject)
            {
                EventManager.TriggerEvent(EventType.Delivery, new Dictionary<EventMessageType, object> { { EventMessageType.UnitedObjects, (interactiveObj as PlateInteractiveObject).UnitedInteractiveObj } });
                ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                return null;
            }
            else
            {
                return interactiveObj;
            }
        }
    }
}
