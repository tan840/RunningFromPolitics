using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed;
    bool canMove = false;
    Rigidbody m_RB;


    SoundManager m_SoundManager;
    public bool CanMove { get => canMove; set => canMove = value; }
    public float MoveSpeed { get => m_MoveSpeed; set => m_MoveSpeed = value; }

    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (canMove)
        {   
            m_RB.velocity = (m_MoveSpeed * Time.fixedDeltaTime * transform.forward);
        }
    }
}
