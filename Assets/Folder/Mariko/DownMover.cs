using UnityEngine;

public class DownMover : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.0001f)] private float _moveForce = 0.001f;

    private void Update()
    {
        transform.Translate(new Vector3(0.0f, -_moveForce, 0.0f));
    }
}