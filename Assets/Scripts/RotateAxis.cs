using UnityEngine;

public class RotateAxis : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private Vector3 _angle;

    private void Start()
    {
        RotateEuler();
        //RotateQuarternion();
    }

    private void RotateQuarternion()
    {
        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);

            point.transform.position = HolisticMath.QRotate(position, new Coords(0, 1, 0), _angle.y).ToVector();
        }
    }

    private void RotateEuler()
    {
        _angle *= Mathf.Deg2Rad;

        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);

            point.transform.position = HolisticMath.Rotate(position, _angle.x, true, _angle.y, true, _angle.z, true).ToVector();
            Matrix rotationMatrix = HolisticMath.GetRotationMatrix(_angle.x, true, _angle.y, true, _angle.z, true);

            float rotationAngle = HolisticMath.GetRotationAngle(rotationMatrix);
            Coords rotationAxis = HolisticMath.GetRotationAxis(rotationMatrix, rotationAngle);

            Debug.Log(rotationAngle * Mathf.Rad2Deg + " about " + rotationAxis.ToString());
            Coords.DrawLine(Coords.zero, rotationAxis * 5f, 0.1f, Color.yellow);
        }
    }
}
