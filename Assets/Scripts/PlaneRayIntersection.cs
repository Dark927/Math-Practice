using UnityEngine;

public class PlaneRayIntersection : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;

    [SerializeField] private Transform _corner_1;
    [SerializeField] private Transform _corner_2;
    [SerializeField] private Transform _corner_3;

    Plane _plane;

    private void Start()
    {
        _plane = new Plane(_corner_1.position, _corner_2.position, _corner_3.position);
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
