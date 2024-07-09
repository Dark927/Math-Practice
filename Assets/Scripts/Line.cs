using UnityEngine;

public class Line
{
    public enum LineType
    { line, lineSegment, ray }


    private Coords _A;
    private Coords _B;
    private Coords _v;

    private LineType _type;

    public Line(Coords A, Coords B, LineType type)
    {
        _A = A;
        _B = B;
        _v = B - A;
        _type = type;
    }

    public Line(Coords A, Vector3 direction, LineType type)
    {
        _A = A;
        _v = new Coords(direction);
        _B = A + _v;
        _type = type;
    }

    public Coords A
    {
        get { return _A; }
    }

    public Coords V
    {
        get { return _v; }
    }


    public Coords Lerp(float t)
    {
        t = Clamp(t, _type);

        return new Coords(_A.X + _v.X * t, _A.Y + _v.Y * t, _A.Z);
    }


    static public Coords Lerp(Coords A, Coords B, float t, LineType type)
    {
        t = Clamp(t, type);

        Coords v = B - A;
        return new Coords(A.X + v.X * t, B.Y + v.Y * t);
    }

    private static float Clamp(float t, LineType type)
    {
        float clampedT = t;

        if (type == LineType.lineSegment)
        {
            clampedT = Mathf.Clamp(t, 0, 1);
        }
        else if (type == LineType.ray && t < 0)
        {
            clampedT = 0;
        }

        return clampedT;
    }

    public void Draw(float width, Color color)
    {
        Coords.DrawLine(_A, _B, width, color);
    }

    public float IntersectAt(Line line)
    {
        Coords perpV = Coords.Perp(line.V);
        float t;

        if (!Mathf.Approximately(perpV * _v, 0))
        {
            Coords c = line.A - _A;
            t = (perpV * c) / (perpV * _v);
            
            if((_type == LineType.lineSegment) && ((t > 1) || t < 0))
            {
                t = float.NaN;
            }
        }
        else
        {
            t = float.NaN;
        }

        return t;
    }
}
