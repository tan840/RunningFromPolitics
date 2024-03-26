using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    [SerializeField] LayerMask m_LayerMask;
    Camera m_Camera;
    RaycastHit m_Hit;
    IGrabbable m_Grabbable;
    float m_ZDistance;
    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity, m_LayerMask))
            {
                m_Grabbable = m_Hit.collider.gameObject.GetComponent<IGrabbable>();
                m_ZDistance = (m_Hit.transform.position - m_Camera.transform.position).z;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_Grabbable = null;
            m_ZDistance = 0;
        }
        m_Grabbable?.OnGrab(m_ZDistance);
    }
}
