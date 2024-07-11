using System.Collections.Generic;
using UnityEngine;

public class TransformationsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private Vector3 _translation;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private Vector3 _shear;
    [SerializeField] private GameObject _center;

    private void Start()
    {
        Transformations();

        DrawHouse();
    }

    private void DrawHouse()
    {
        List<Line> lineList = new List<Line>();
        HouseLines(lineList);

        foreach (Line line in lineList)
        {
            line.Draw(0.1f, Color.yellow);
        }
    }

    private void HouseLines(List<Line> lineList)
    {
        lineList.Add(new Line(new Coords(_points[0].transform.position), new Coords(_points[1].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[0].transform.position), new Coords(_points[2].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[1].transform.position), new Coords(_points[3].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[3].transform.position), new Coords(_points[2].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[2].transform.position), new Coords(_points[6].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[6].transform.position), new Coords(_points[7].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[7].transform.position), new Coords(_points[5].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[5].transform.position), new Coords(_points[1].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[5].transform.position), new Coords(_points[4].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[4].transform.position), new Coords(_points[6].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[4].transform.position), new Coords(_points[0].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[7].transform.position), new Coords(_points[3].transform.position), Line.LINETYPE.SEGMENT));

        lineList.Add(new Line(new Coords(_points[6].transform.position), new Coords(_points[9].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[9].transform.position), new Coords(_points[7].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[9].transform.position), new Coords(_points[8].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[8].transform.position), new Coords(_points[2].transform.position), Line.LINETYPE.SEGMENT));
        lineList.Add(new Line(new Coords(_points[8].transform.position), new Coords(_points[3].transform.position), Line.LINETYPE.SEGMENT));
    }

    private void Transformations()
    {
        Vector3 centerPosition = _center.transform.position;

        _rotation *= Mathf.Deg2Rad;

        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);


            //position = HolisticMath.Translate(position, new Coords(-centerPosition.x, -centerPosition.y, -centerPosition.z, 0));
            //position = HolisticMath.Rotate(position, _rotation.x, true, _rotation.y, false, _rotation.z, true);
            point.transform.position = HolisticMath.Shear(position, _shear.x, _shear.y, _shear.z).ToVector();
        }
    }
}
