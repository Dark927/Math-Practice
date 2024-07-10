using UnityEngine;

public class CreatePlaneHit : MonoBehaviour
{
    [SerializeField] private Transform _A;
    [SerializeField] private Transform _B;
    [SerializeField] private Transform _C;
    [SerializeField] private Transform _D;
    [SerializeField] private Transform _E;
    [SerializeField] private GameObject _ball;

    [Space]

    [SerializeField] private float _planeSpheresStep = 0.01f;
    [SerializeField] private float _planeMaxArea = 1f;

    private Plane _plane;
    private Line _line;
    private Line _trajectory;



    private void Start()
    {
        CreatePlane();
        CreateLine();

        DisplayIntersection();
    }

    private void Update()
    {
        float t = Time.time;

        if (t <= 1)
        {
            _ball.transform.position = _trajectory.Lerp(t).ToVector();
        }
        else
        {
            Coords normal = HolisticMath.Cross(new Coords(_B.position - _A.position), new Coords(_C.position - _A.position));
            _ball.transform.position += _trajectory.Reflect(normal).ToVector() * Time.deltaTime * 16f;
        }
    }


    private void DisplayIntersection()
    {
        float intersectionT = _line.IntersectsAt(_plane);

        if (!float.IsNaN(intersectionT))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<MeshRenderer>().material.color = Color.yellow;
            cube.transform.position = _line.Lerp(intersectionT).ToVector();

            _trajectory = new Line(_line.A, _line.Lerp(intersectionT), Line.LINETYPE.SEGMENT);
        }
    }

    private void CreateLine()
    {
        _line = new Line(new Coords(_D.position), new Coords(_E.position), Line.LINETYPE.RAY);
        _line.Draw(0.5f, Color.green);
    }

    private void CreatePlane()
    {
        _plane = new Plane(new Coords(_A.position), new Coords(_B.position), new Coords(_C.position));

        for (float t = 0; t < _planeMaxArea; t += _planeSpheresStep)
        {
            for (float s = 0; s < _planeMaxArea; s += _planeSpheresStep)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = _plane.Lerp(s, t).ToVector();
            }
        }
    }
}
