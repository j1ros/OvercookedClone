using Overcooked.Data;

namespace Overcooked.InteractivObject
{
    public interface IUnited
    {
        public bool AddInteractiveObject(InteractiveSO interactiveObj);
        public void Clear();
        public InteractiveObject PlacedInteractiveObject { get; }
    }
}
