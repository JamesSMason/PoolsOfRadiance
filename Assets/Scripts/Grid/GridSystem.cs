using UnityEngine;

namespace PoR.Grid
{
    public class GridSystem
    {
        private int height;
        private int width;
        private float cellSize;

        private GridObject[,] gridObjects;

        public GridSystem(int height, int width, float cellSize)
        {
            this.height = height;
            this.width = width;
            this.cellSize = cellSize;

            gridObjects = new GridObject[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    gridObjects[x,z] = new GridObject(this, gridPosition);
                }
            }
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                    GridObjectDebug gridObjectDebug = debugTransform.GetComponent<GridObjectDebug>();
                    gridObjectDebug.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return gridObjects[gridPosition.x,gridPosition.z];
        }
    }
}