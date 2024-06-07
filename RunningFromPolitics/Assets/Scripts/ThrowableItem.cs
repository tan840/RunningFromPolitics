using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ThrowableItem : MonoBehaviour, IGrabbable
{
    [SerializeField] string m_NameTag;
    [SerializeField] float m_SpeedMuiltiplier = 20f;
    protected bool hasCollided = false;
    Camera m_camera;
    Vector3 m_TouchPosition;
    Transform m_Tr;
    Rigidbody m_RB;
    Vector3 m_WorldPosition, m_MovePosition;
    [SerializeField] ParticleSystem m_ParticleSystem;

    public Rigidbody RB { get => m_RB; set => m_RB = value; }

    public string ItemTag => m_NameTag;

    SoundManager m_soundManager;


    private void Start()
    {
        m_soundManager = SoundManager.Instance;
        m_Tr = transform;
        m_ParticleSystem?.Stop();
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
        m_MovePosition = (m_WorldPosition - transform.position);
        //m_RB.MovePosition(m_MovePosition);
        RB.AddForce(m_MovePosition.x * m_SpeedMuiltiplier, m_MovePosition.y * m_SpeedMuiltiplier, 0f, ForceMode.Acceleration);
        //soundmanager has grab shound played
        //Has_Grabbed = true;
        // m_soundManager.Play("MindControl");
        if (m_ParticleSystem != null && !m_ParticleSystem.isPlaying) { m_ParticleSystem?.Play();}
        

    }

    public virtual void OnGrabRelease()
    {
        m_ParticleSystem?.Stop();
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.layer == 7 && collision.collider.TryGetComponent(out PlayerHealth health))
        {
            hasCollided = true;
            health.TakeHit();

        }
    }

    public Transform GetTransform()
    {
        return m_Tr;
    }

}
