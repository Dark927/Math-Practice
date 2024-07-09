using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    Line wall;
    Line ballPath;
    public GameObject ball;
    private Line _trajectory;

    // Start is called before the first frame update
    void Start()
    {
        wall = new Line(new Coords(5, -2, 0), new Coords(0, 5, 0));
        wall.Draw(1, Color.blue);

        ballPath = new Line(new Coords(-6, 0, 0), new Coords(100, 0, 0));
        ballPath.Draw(0.1f, Color.yellow);

        ball.transform.position = ballPath.Lerp(0).ToVector();


        float _intersectionT = ballPath.IntersectsAt(wall);
        float _intersectionS = wall.IntersectsAt(ballPath);

        if (!float.IsNaN(_intersectionT) && !float.IsNaN(_intersectionS))
        {
            _trajectory = new Line(ballPath.A, ballPath.Lerp(_intersectionT), Line.LINETYPE.SEGMENT);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ball.transform.position = _trajectory.Lerp(Time.time * 0.1f).ToVector();
    }
}