using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float balance = 0f; // Range is -1 (Left) to +1 (Right)

    [SerializeField] private float balanceSpeed = 3f;

    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    [SerializeField] private float balanceInputIncrement = 0.6f;

    [Header("Enhanced Randomness")]
    [SerializeField] private float baseRandomRange = 0.1f;
    // Multiplier for the current balance value. A higher value means the positive
    // feedback is stronger (harder to recover).
    [SerializeField] private float momentumBiasMultiplier = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Calculate the Momentum Bias
        // If balance is +0.5 (tilting right), the bias is +0.25 (0.5 * 0.5)
        // If balance is -0.8 (tilting left), the bias is -0.4  (-0.8 * 0.5)
        float momentumBias = balance * momentumBiasMultiplier;

        // 2. Calculate the Random/Environmental Fluctuation
        // The random range is now shifted by the momentumBias.
        float minRandom = -baseRandomRange;
        float maxRandom = baseRandomRange;

        // The final random value will be between (minRandom + momentumBias) and (maxRandom + momentumBias)
        float totalRandomChange = UnityEngine.Random.Range(minRandom, maxRandom) + momentumBias;

        // 3. Apply the combined environmental and momentum-based change
        balance += totalRandomChange * Time.deltaTime;

        // 4. Apply Player Input
        if (Input.GetKey(leftKey))
        {
            balance -= balanceInputIncrement * Time.deltaTime;
        }

        if (Input.GetKey(rightKey))
        {
            balance += balanceInputIncrement * Time.deltaTime;
        }

        // 5. Clamp and Apply Movement
        balance = Mathf.Clamp(balance, -1f, 1f);

        // Correct way to set Rigidbody2D velocity (assuming 2D platformer)
        rb.linearVelocity = new Vector2(balance * balanceSpeed, rb.linearVelocity.y);
    }
}