using UnityEngine;

public class TransformationsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private Vector3 _translation;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private GameObject _center;

    private void Start()
    {
        Vector3 centerPosition = _center.transform.position;

        _rotation *= Mathf.Deg2Rad;

        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);


            position = HolisticMath.Translate(position, new Coords(-centerPosition.x, -centerPosition.y, -centerPosition.z, 0));
            position = HolisticMath.Rotate(position, _rotation.x, true, _rotation.y, false, _rotation.z, true);
            point.transform.position = HolisticMath.Translate(position, new Coords(centerPosition.x, centerPosition.y, centerPosition.z, 0)).ToVector();
        }
    }
}
