using System;
using UnityEngine;

public class TeaSpill : MonoBehaviour
{
    public int failureThreshold = 100;
    public int spillCount = 0;

    [SerializeField] private string teaSpillKey = "TeaSpill";
    [SerializeField] private TeaReceive teaReceive; // Reference to TeaReceive script to reset its count on failure

    private void Awake()
    {
        if (!teaReceive)
        {
            teaReceive = FindAnyObjectByType<TeaReceive>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            // Handle tea spill logic here (e.g., reduce score, play sound, etc.)
            Destroy(other.gameObject);
            spillCount++;
            if (spillCount >= failureThreshold)
            {
                PlayerPrefs.SetFloat(teaSpillKey, spillCount);
                teaReceive.receivedTeaCountSave();
                // go to result scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
                // You can add additional game over handling here
            }
        }
    }
}