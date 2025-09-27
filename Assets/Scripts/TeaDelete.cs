using UnityEngine;

public class TeaDelete : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            // Handle tea spill logic here (e.g., reduce score, play sound, etc.)
            Destroy(other.gameObject);
        }
    }
}