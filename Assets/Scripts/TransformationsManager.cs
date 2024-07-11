using UnityEngine;

public class TransformationsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private float _angle;
    [SerializeField] private Vector3 _translation;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private GameObject _center;

    private void Start()
    {
        Vector3 centerPosition = _center.transform.position;

        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);

            position = HolisticMath.Translate(position, new Coords(
                -centerPosition.x,
                -centerPosition.y,
                -centerPosition.z, 
                1));

            position = HolisticMath.Scale(position, _scale.x, _scale.y, _scale.z);

            point.transform.position = HolisticMath.Translate(position, new Coords(
                centerPosition.x,
                centerPosition.y,
                centerPosition.z,
                1)).ToVector();
        }
    }
}
