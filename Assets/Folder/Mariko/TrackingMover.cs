using UnityEngine;

public class TrackingMover : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.0001f)] private float _moveForce = 0.001f;
    [SerializeField] private GameObject _target;

    private void Awake()
    {
        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            transform.Translate(_moveForce * direction.normalized);
        }
    }
}
