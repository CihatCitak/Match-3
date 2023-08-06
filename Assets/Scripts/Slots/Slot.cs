using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Slots
{
    public abstract class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Image slotImage;

        private Color oldColor;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            oldColor = slotImage.color;
            slotImage.color = Color.white;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            slotImage.color = oldColor;
        }
    }
}
