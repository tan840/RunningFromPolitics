using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour, IGrabbable
{
    //[SerializeField] float m_zDistance;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] Camera m_camera;
    [SerializeField] Vector3 m_TouchPosition;

    Rigidbody m_RB;
    Vector3 m_WorldPosition, m_MovePosition;

    public Rigidbody RB { get => m_RB; set => m_RB = value; }

    void Awake()
    {
        m_camera = Camera.main;      
        m_RB = GetComponent<Rigidbody>();
    }
    public void OnGrab(float _zDistance)
    {
        m_TouchPosition = Input.mousePosition;
        m_TouchPosition.z = _zDistance;
        m_WorldPosition = m_camera.ScreenToWorldPoint(m_TouchPosition);
        m_MovePosition = Vector3.Lerp(m_RB.position, m_WorldPosition, m_MoveSpeed);
        m_RB.MovePosition(m_MovePosition);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //print("Player");
        }
    }
}
