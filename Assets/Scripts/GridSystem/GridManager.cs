using Slots;
using UnityEngine;
using System.Collections.Generic;

namespace GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float padding;
        [SerializeField] private List<Slot> slotPrefabs;
        [SerializeField] private SafeArea safeAreaCalculater;

        private const float slotReferenceSize = 1f;
        private List<Slot> grid = new List<Slot>();

        private void Start()
        {
            var safeAreaWidth = safeAreaCalculater.GetWidth();

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

                    var slot = Instantiate(slotPrefabs[Random.Range(0, slotPrefabs.Count)], transform);

                    slot.transform.localScale = Vector3.one * (newCellSize / slotReferenceSize);
                    slot.transform.localPosition = new Vector3(nextPositionX, nextPositionY, 0f);

                    slot.name = string.Format("Cell: {0} , {1}", i, j);

                    grid.Add(slot);
                }
            }
        }
    }
}
