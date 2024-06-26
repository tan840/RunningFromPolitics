using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ThrowableItem : MonoBehaviour, IGrabbable
{
    //[SerializeField] float m_zDistance;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] string m_NameTag;
    bool hasCollided = false;
    Camera m_camera;
    Vector3 m_TouchPosition;

    Rigidbody m_RB;
    Vector3 m_WorldPosition, m_MovePosition;

    public Rigidbody RB { get => m_RB; set => m_RB = value; }

    public string ItemTag => m_NameTag;

    SoundManager m_soundManager;

    bool Has_Grabbed = false;

    private void Start()
    {
        m_soundManager = SoundManager.Instance;
    }

    public virtual void Awake()
    {

        m_camera = Camera.main;      
        m_RB = GetComponent<Rigidbody>();
    }

    

    public virtual void OnGrab(float _zDistance)
    {
        m_TouchPosition = Input.mousePosition;
        m_TouchPosition.z = _zDistance;
        m_WorldPosition = m_camera.ScreenToWorldPoint(m_TouchPosition);
        m_MovePosition = Vector3.Lerp(m_RB.position, m_WorldPosition, m_MoveSpeed);
        m_RB.MovePosition(m_MovePosition);
        //soundmanager has grab shound played
        Has_Grabbed = true;
       // m_soundManager.Play("MindControl");

    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.layer == 7 && collision.collider.TryGetComponent(out PlayerHealth health))
        {
            hasCollided = true;
            health.TakeHit();
            
        }
    }
}
