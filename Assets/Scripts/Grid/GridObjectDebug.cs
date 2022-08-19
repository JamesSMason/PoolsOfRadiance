using UnityEngine;
using TMPro;

namespace PoR.Grid
{
    public class GridObjectDebug : MonoBehaviour
    {
        [SerializeField] private TextMeshPro positionText = null;

        private GridObject gridObject;

        public void SetGridObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
        }

        private void Update()
        {
            positionText.text = gridObject.ToString();
        }
    }
}