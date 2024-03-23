using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed;

    Rigidbody m_RB;
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_RB.AddForce(m_MoveSpeed * Time.fixedDeltaTime * transform.forward, ForceMode.VelocityChange);
    }
}
