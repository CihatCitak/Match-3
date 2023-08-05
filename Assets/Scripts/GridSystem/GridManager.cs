using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float padding;
        [SerializeField] private RectTransform safeArea;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] CanvasScaler canvasScaler;

        private const float cellReferenceSize = 100f;
        private List<GameObject> grid = new List<GameObject>();

        private void Start()
        {
            Application.targetFrameRate = 1000;
            var safeAreaWidth = canvasScaler.referenceResolution.x + safeArea.sizeDelta.x;
            //var safeAreaHeight = canvasScaler.referenceResolution.y + safeArea.sizeDelta.y;

            var totalPaddingWidth = (gridSize.x - 1) * padding;
            var totalPaddingHeight = (gridSize.y - 1) * padding;
            var newCellSize = (safeAreaWidth - totalPaddingWidth) / gridSize.x;

            // Soldan Sağa dizmeye başladığımız senaryoya göre işlemleri ilerletiyoruz
            var startPositionX = (safeAreaWidth - newCellSize) / 2f;

            var totalNeedHeight = gridSize.y * newCellSize + totalPaddingHeight;
            var startPositionY = (totalNeedHeight - newCellSize) / 2;


            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    var nextPositionX = -startPositionX + (newCellSize + padding) * j;
                    var nextPositionY = startPositionY - (newCellSize + padding) * i;

                    var cell = Instantiate(cellPrefab, safeArea);

                    cell.transform.localScale = Vector3.one * (newCellSize / cellReferenceSize);
                    cell.transform.localPosition = new Vector3(nextPositionX, nextPositionY, 0f);

                    cell.name = string.Format("Cell: {0} , {1}", i, j);

                    grid.Add(cell);
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
