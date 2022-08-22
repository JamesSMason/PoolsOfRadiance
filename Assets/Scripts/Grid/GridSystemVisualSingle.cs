using UnityEngine;

namespace PoR.Grid
{
    public class GridSystemVisualSingle : MonoBehaviour
    {
        [SerializeField] MeshRenderer meshRenderer = null;

        public void Show()
        {
            meshRenderer.enabled = true;
        }

        public void Hide()
        {
            meshRenderer.enabled = false;
        }
    }
}