using UnityEngine;

public class TeaReceive : MonoBehaviour
{
    public int ReceivedTeaCount = 0;
    [SerializeField] private GameObject particleEffect;
    private float particleEffectDuration = 1.5f;
    private float particleEffectTimer = 0.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TeaBall>())
        {
            Destroy(other.gameObject);
            ReceivedTeaCount++;
            if(!particleEffect.activeInHierarchy)
            {
                particleEffect.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (particleEffect.activeInHierarchy)
        {
            particleEffectTimer += Time.deltaTime;
            if (particleEffectTimer >= particleEffectDuration)
            {
                particleEffect.SetActive(false);
                particleEffectTimer = 0.0f;
            }
        }
    }
}
