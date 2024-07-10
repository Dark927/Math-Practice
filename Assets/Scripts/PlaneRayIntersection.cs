using UnityEngine;

public class PlaneRayIntersection : MonoBehaviour
{
    [SerializeField] private GameObject _sheep;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0.3f, 0);

    [SerializeField] private Vector2 _boundsX = new Vector3(-7f, 7f);
    [SerializeField] private Vector2 _boundsZ = new Vector3(-7f, 7f);

    Plane _plane;

    private void Start()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;

        _plane = new Plane(
            transform.TransformPoint(vertices[0]) + _offset,
            transform.TransformPoint(vertices[1]) + _offset,
            transform.TransformPoint(vertices[2]) + _offset);
    }

    private void Update()
    {
        DragSheepLogic();
    }

    private void DragSheepLogic()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float t = 0f;

            if (_plane.Raycast(ray, out t))
            {
                Vector3 intersectionPoint = ray.GetPoint(t);

                bool outOfBoundsX = (intersectionPoint.x < _boundsX.x) || (_boundsX.y < intersectionPoint.x);
                bool outOfBoundsZ = (intersectionPoint.z < _boundsZ.x) || (_boundsZ.y < intersectionPoint.z);

                if (!(outOfBoundsX || outOfBoundsZ))
                {
                    _sheep.transform.position = intersectionPoint;
                }
            }
        }
    }
}
