using UnityEngine;

namespace GridSystem
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] Transform leftCorner;
        [SerializeField] Transform rightCorner;

        private Vector2 leftCornerWorldPos => mainCamera.ScreenToWorldPoint((Vector2) leftCorner.position);
        private Vector2 rightCornerWorldPos => mainCamera.ScreenToWorldPoint((Vector2) rightCorner.position);

        public float GetWidth()
        {
            return Mathf.Abs(leftCornerWorldPos.x - rightCornerWorldPos.x);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(leftCornerWorldPos, .1f);
            Gizmos.DrawSphere(rightCornerWorldPos, .1f);
        }
#endif
    }
}
