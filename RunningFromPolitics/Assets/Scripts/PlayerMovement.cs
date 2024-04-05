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

    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_SoundManager = SoundManager.Instance;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            /*  m_SoundManager.PlayAudio(0);
              Invoke(ToString(m_SoundManager.PlayAudio(0),1f);*/
            //  SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
           //m_SoundManager.PlayAudio(0,5 );
            
            m_RB.velocity = (m_MoveSpeed * Time.fixedDeltaTime * transform.forward);
        }
    }
}
