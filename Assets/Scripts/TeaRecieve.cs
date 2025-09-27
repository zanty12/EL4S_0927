using UnityEngine;

public class TeaRecieve : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            Destroy(other.gameObject);
        }
    }
}
