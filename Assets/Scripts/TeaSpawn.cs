using UnityEngine;

public class TeaSpawn : MonoBehaviour
{
    [SerializeField] GameObject teaPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float spawnInterval = 0.1f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Instantiate(teaPrefab, spawnPoint.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
