using UnityEngine;

public class Coords
{
    private float _x;
    private float _y;
    private float _z;

    public Coords(float x, float y, float z = -1)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public Coords(Vector3 coords)
    {
        _x = coords.x;
        _y = coords.y;
        _z = coords.z;
    }

    public float X
    { get { return _x; } }
    public float Y
    { get { return _y; } }
    public float Z
    { get { return _z; } }

    public override string ToString()
    {
        return $"({_x},{_y},{_z})";
    }

    public Vector3 ToVector()
    {
        return new Vector3(_x, _y, _z);
    }

    static public Coords Perp(Coords v)
    {
        return new Coords(-v.Y, v.X, v.Z);
    }

    static public void DrawPoint(Coords position, float width, Color color)
    {
        GameObject point = new GameObject("Point_" + position.ToString());
        LineRenderer lineRenderer = ConfigureLineRenderer(point, width, color);

        lineRenderer.SetPosition(0, new Vector3(position._x + width / 3f, position._y + width / 3f, position._z));
        lineRenderer.SetPosition(1, new Vector3(position._x - width / 3f, position._y - width / 3f, position._z));
    }

    static public void DrawLine(Coords startPosition, Coords endPosition, float width, Color color)
    {
        GameObject line = new GameObject("Line_" + startPosition.ToString() + '_' + endPosition.ToString());
        LineRenderer lineRenderer = ConfigureLineRenderer(line, width, color);

        lineRenderer.SetPosition(0, new Vector3(startPosition._x, startPosition._y, startPosition._z));
        lineRenderer.SetPosition(1, new Vector3(endPosition._x, endPosition._y, endPosition._z));
    }

    static private LineRenderer ConfigureLineRenderer(GameObject line, float width, Color color)
    {
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        return lineRenderer;
    }

    static public Coords operator +(Coords first, Coords second)
    {
        return new Coords(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
    }

    static public float operator *(Coords first, Coords second)
    {
        return (first.X * second.X) + (first.Y * second.Y) + (first.Z * second.Z);
    }

    static public Coords operator -(Coords first, Coords second)
    {
        return new Coords(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
    }
}
