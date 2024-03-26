using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed;
    bool canMove = false;
    Rigidbody m_RB;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            m_RB.AddForce(m_MoveSpeed * Time.fixedDeltaTime * transform.forward, ForceMode.VelocityChange);
        }
    }
}
