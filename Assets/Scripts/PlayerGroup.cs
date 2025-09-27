using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGroup : MonoBehaviour
{
    [SerializeField] private GameObject pot;
    private Rigidbody2D potRb;
    private float potY;
    [SerializeField] private GameObject cup;
    private Rigidbody2D cupRb;
    private float cupY;
    private Rigidbody2D self;

    [SerializeField] private float moveSpeed = 2f;

    private void Awake()
    {
        self = GetComponent<Rigidbody2D>();
        potRb = pot.GetComponent<Rigidbody2D>();
        cupRb = cup.GetComponent<Rigidbody2D>();
        potY = pot.transform.localPosition.y;
        cupY = cup.transform.localPosition.y;
    }

    private void Start()
    {
        // move self,pot, cup at constant speed upwards
        self.linearVelocityY = moveSpeed;
        potRb.linearVelocityY = self.linearVelocityY;
        cupRb.linearVelocityY = self.linearVelocityY;
    }

    // Update is called once per frame
    void Update()
    {
        //get the local position of pot and cup and adjust their velocity to maintain their y offset
        Vector3 potPos = pot.transform.localPosition;
        Vector3 cupPos = cup.transform.localPosition;
        potRb.linearVelocityY += (potY - potPos.y) * moveSpeed;
        cupRb.linearVelocityY += (cupY - cupPos.y) * moveSpeed;
    }
}
