using UnityEngine;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;

        private IFingerDown fingerDownSlot;
        private Collider2D touchedCollider;

        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchedCollider = Physics2D.OverlapPoint(touchWorldPosition);

                if (touchedCollider == null)
                    return;

                if (touchedCollider.TryGetComponent(out IFingerDown fingerDownObject))
                {
                    fingerDownSlot = fingerDownObject;
                    fingerDownObject.FingerDown();
                }

                return;
            }

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (fingerDownSlot == null)
                    return;

                if (touchedCollider.TryGetComponent(out IFingerUp fingerUpObject))
                {
                    fingerUpObject.FingerUp();
                }

                return;
            }
        }
    }
}
