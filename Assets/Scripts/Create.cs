using UnityEngine;

public class Create : MonoBehaviour
{
    // Plane 

    [Space]
    [Header("Plane Settings")]
    [Space]

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private Transform _thirdPoint;


    // Lines

    Line L1;
    Line L2;


    // Start is called before the first frame update
    void Start()
    {
        //DrawSphericPlane();

        DrawLines();
    }

    private void DrawLines()
    {
        L1 = new Line(new Coords(1f, 4f, 0), new Coords(7f, 0.5f, 0), Line.LineType.lineSegment);
        L1.Draw(1, Color.green);

        L2 = new Line(new Coords(0, 0, 0), new Coords(7f, 5f, 0), Line.LineType.lineSegment);
        L2.Draw(1, Color.red);


        float t = L1.IntersectAt(L2);
        float s = L2.IntersectAt(L1);

        if (!float.IsNaN(t) && !float.IsNaN(s))
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.GetComponent<MeshRenderer>().material.color = Color.yellow;
            sphere.transform.position = L1.Lerp(t).ToVector();
        }
    }

    private void DrawSphericPlane()
    {
        Plane plane = new Plane(new Coords(_startPoint.position), new Coords(_secondPoint.position), new Coords(_thirdPoint.position));

        for (float s = 0; s < 1f; s += 0.1f)
        {
            for (float t = 0; t < 1f; t += 0.1f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = plane.Lerp(s, t).ToVector();
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
