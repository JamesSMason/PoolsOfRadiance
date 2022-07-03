using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance { get; private set; }

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"This is not the only version of MouseWorld. {transform} - {Instance}.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Instance.mousePlaneLayerMask);

        return hit.point;
    }
}