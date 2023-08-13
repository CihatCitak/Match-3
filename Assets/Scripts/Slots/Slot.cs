using UnityEngine;
using InputSystem;

namespace Slots
{
    public abstract class Slot : MonoBehaviour, IFingerDown, IFingerUp, IFingerSwap
    {
        public abstract void FingerDown();

        public abstract void FingerUp();

        public abstract void FingerSwap();
    }
}
