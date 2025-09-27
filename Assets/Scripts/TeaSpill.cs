using System;
using UnityEngine;

public class TeaSpill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            // Handle tea spill logic here (e.g., reduce score, play sound, etc.)
            Destroy(other.gameObject);
        }
    }
}
