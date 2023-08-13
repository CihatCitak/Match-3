using UnityEngine;

namespace Slots
{
    public class ColorSlot : Slot
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] ColorType colorType;

        public override void FingerDown()
        {
            Debug.Log("FingerDown: " +  name);
        }

        public override void FingerUp()
        {
            Debug.Log("FingerUp: " + name);
        }

        public override void FingerSwap()
        {
            Debug.Log("FingerSwap: " + name);
        }
    }
}
