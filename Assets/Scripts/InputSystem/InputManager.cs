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

                fingerDownSlot = null;

                return;
            }

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (fingerDownSlot == null)
                    return;

                Vector2 deltaPos= Input.GetTouch(0).deltaPosition;

                if (deltaPos.x > 1)
                {
                    Debug.Log("Right");
                }
                else if (deltaPos.x < -1)
                {
                    Debug.Log("Left");
                }
                else if (deltaPos.y > 1)
                {
                    Debug.Log("Up");
                }
                else if (deltaPos.y < -1)
                {
                    Debug.Log("Down");
                }

                fingerDownSlot = null;
            }
        }
    }
}
