using UnityEngine;

public class TeaReceive : MonoBehaviour
{
    public int ReceivedTeaCount = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            Destroy(other.gameObject);
            ReceivedTeaCount++;
        }
    }
}
