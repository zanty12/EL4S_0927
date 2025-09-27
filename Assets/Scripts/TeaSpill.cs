using System;
using UnityEngine;

public class TeaSpill : MonoBehaviour
{
    public int failureThreshold = 100;
    public int spillCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            // Handle tea spill logic here (e.g., reduce score, play sound, etc.)
            Destroy(other.gameObject);
            spillCount++;
            if (spillCount >= failureThreshold)
            {
                // go to result scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
                // You can add additional game over handling here
            }
        }
    }
}