using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchControl : MonoBehaviour
{
    [SerializeField] LayerMask m_LayerMask;
    [SerializeField] float m_Grablenth = 100;
    Camera m_Camera;
    RaycastHit m_Hit;
    IGrabbable m_Grabbable;
    float m_ZDistance;
    SoundManager m_soundManager;
    private void Start()
    {
        m_Camera = Camera.main;
        m_soundManager = SoundManager.Instance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastSingle();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_Grabbable = null;
            m_ZDistance = 0;
            m_soundManager.Stop("MindControl");
        }
        m_Grabbable?.OnGrab(m_ZDistance);
        if (Input.GetMouseButton(0))
        {
            RaycastConst();
        }
    }
    void RaycastSingle()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out m_Hit, m_Grablenth, m_LayerMask))
        {
            Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.position + ray.direction, Color.red, m_Grablenth);
            if (m_Hit.collider.gameObject.TryGetComponent(out IGrabbable GrabItem))
            {
                m_Grabbable = GrabItem;
                m_ZDistance = (m_Grabbable.GetTransform().position - m_Camera.transform.position).z;
                m_soundManager.Play("MindControl");
            }
        }
    }
    void RaycastConst()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out m_Hit, m_Grablenth, m_LayerMask))
        {
            if (m_Hit.collider.gameObject.TryGetComponent(out Icollectable Coin))
            {
                Coin.MoveCoinToCanvas();
            }
        }
    }
}
