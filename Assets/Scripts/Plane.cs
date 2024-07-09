
public class Plane
{
    private Coords _A;
    private Coords _v;
    private Coords _u;

    public Plane(Coords startPoint, Coords pointB, Coords pointC)
    {
        _A = startPoint;
        _v = pointB - startPoint;
        _u = pointC - startPoint;
    }



    public  Coords Lerp(float s, float t)
    {
        float xst = _A.X + _v.X * s + _u.X * t;
        float yst = _A.Y + _v.Y * s + _u.Y * t;
        float zst = _A.Z + _v.Z * s + _u.Z * t;

        return new Coords(xst, yst, zst);
    }
}
