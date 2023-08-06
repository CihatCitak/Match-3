using Slots;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float padding;
        [SerializeField] private RectTransform safeArea;
        [SerializeField] private List<Slot> slotPrefabs;
        [SerializeField] CanvasScaler canvasScaler;

        private const float slotReferenceSize = 100f;
        private List<Slot> grid = new List<Slot>();

        private void Start()
        {
            Application.targetFrameRate = 1000;
            var safeAreaWidth = canvasScaler.referenceResolution.x + safeArea.sizeDelta.x;

            var totalPaddingWidth = (gridSize.x - 1) * padding;
            var totalPaddingHeight = (gridSize.y - 1) * padding;
            var newCellSize = (safeAreaWidth - totalPaddingWidth) / gridSize.x;

            var startPositionX = (safeAreaWidth - newCellSize) / 2f;

            var totalNeedHeight = gridSize.y * newCellSize + totalPaddingHeight;
            var startPositionY = (totalNeedHeight - newCellSize) / 2;


            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    var nextPositionX = -startPositionX + (newCellSize + padding) * j;
                    var nextPositionY = startPositionY - (newCellSize + padding) * i;

                    var slot = Instantiate(slotPrefabs[Random.Range(0, slotPrefabs.Count)], safeArea);

                    slot.transform.localScale = Vector3.one * (newCellSize / slotReferenceSize);
                    slot.transform.localPosition = new Vector3(nextPositionX, nextPositionY, 0f);

                    slot.name = string.Format("Cell: {0} , {1}", i, j);

                    grid.Add(slot);
                }
            }
        }

        private void OnValidate()
        {
            if (EditorApplication.isPlaying && grid != null)
            {
                foreach (var item in grid)
                {
                    Destroy(item);
                }

                grid.Clear();
                Start();
            }
        }
    }
}
