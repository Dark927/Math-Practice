using UnityEngine;

public class DrawLines : MonoBehaviour
{
    Coords centerPoint = new Coords(0f, 0f);
    float _pointWidth = 2f;


    Coords startPointX = new Coords(-160f, 0f);
    Coords endPointX = new Coords(160f, 0f);

    Coords startPointY = new Coords(0f, -100f);
    Coords endPointY = new Coords(0f, 100f);

    float _lineWidth = 0.5f;


    void Start()
    {
        Coords.DrawPoint(centerPoint, _pointWidth, Color.white);
        Coords.DrawLine(startPointX, endPointX, _lineWidth, Color.red);
        Coords.DrawLine(startPointY, endPointY, _lineWidth, Color.green);
    }
}
