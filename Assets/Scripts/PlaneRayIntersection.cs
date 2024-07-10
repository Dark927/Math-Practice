using System.Collections.Generic;
using UnityEngine;

public class PlaneRayIntersection : MonoBehaviour
{
    [SerializeField] private GameObject _sheep;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0.3f, 0);

    [SerializeField] List<Transform> _borderList;
    List<Vector3> _normalsList;

    Plane _plane;

    private void Start()
    {
        CreatePlane();
        _normalsList = GetBordersNormals();
    }

    private List<Vector3> GetBordersNormals()
    {
        List<Vector3> normalsList = new List<Vector3>();

        foreach (Transform border in _borderList)
        {
            Vector3 normal = border.gameObject.GetComponent<MeshFilter>().mesh.normals[0];
            normal = border.TransformVector(normal);

            normalsList.Add(normal);
        }

        return normalsList;
    }

    private void CreatePlane()
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

            if (_plane.Raycast(ray, out float t))
            {
                Vector3 intersectionPoint = ray.GetPoint(t);
                bool insideBorders = IsInsideBorders(intersectionPoint);


                if (insideBorders)
                {
                    _sheep.transform.position = intersectionPoint;
                }
            }
        }
    }

    private bool IsInsideBorders(Vector3 intersectionPoint)
    {
        bool insideBorders = true;

        for (int borderIndex = 0; borderIndex < _borderList.Count; ++borderIndex)
        {
            Vector3 hitPointToBorder = _borderList[borderIndex].position - intersectionPoint;
            insideBorders = insideBorders && Vector3.Dot(hitPointToBorder, _normalsList[borderIndex]) <= 0;
        }

        return insideBorders;
    }
}
