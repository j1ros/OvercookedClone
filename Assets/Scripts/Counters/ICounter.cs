using Overcooked.InteractivObject;

namespace Overcooked.Counter
{
    public interface ICounter
    {
        public InteractiveObject Interapt(InteractiveObject interactiveObj);
        public bool CanAction();
        public void Action();
        public void StopAction();
    }
}
