using UnityEngine;

public class TransformationsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private float _angle;
    [SerializeField] private Vector3 _translation;

    private void Start()
    {
        foreach (GameObject point in _points)
        {
            Coords position = new Coords(point.transform.position, 1);
            point.transform.position = HolisticMath.Translate(position, new Coords(_translation, 1)).ToVector();
        }
    }
}
