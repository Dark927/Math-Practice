using UnityEngine;

public class PlaneRayIntersection : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;

    Plane _plane;

    private void Start()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;

        _plane = new Plane(
            transform.TransformPoint(vertices[0]),
            transform.TransformPoint(vertices[1]),
            transform.TransformPoint(vertices[2]));
    }

    private void Update()
    {
        // Move sphere while holding mouse

        if(Input.GetMouseButton(0))
        {
            // Cast ray from mouse position 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float t = 0f;

            // Check intersection of ray with plane and move sphere

            if(_plane.Raycast(ray, out t))
            {
                Vector3 hitPoint = ray.GetPoint(t);
                _sphere.transform.position = hitPoint;
            }
        }
    }
}
