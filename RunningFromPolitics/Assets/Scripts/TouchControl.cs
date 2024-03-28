using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour
{
    [SerializeField] LayerMask m_LayerMask;
    Camera m_Camera;
    RaycastHit m_Hit;
    IGrabbable m_Grabbable;
    float m_ZDistance;
    EventSystem m_EventSystem;
    private void Start()
    {
        m_Camera = Camera.main;
        m_EventSystem = EventSystem.current;
    }
    private void Update()
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
            CheckMouseOverButton();
            m_Grabbable = null;
            m_ZDistance = 0;
        }
        m_Grabbable?.OnGrab(m_ZDistance);       
    }
    void CheckMouseOverButton()
    {
        if (m_EventSystem.IsPointerOverGameObject())
        {
            PointerEventData pointerdata = new PointerEventData(m_EventSystem);
            pointerdata.position = Input.mousePosition;
            List<RaycastResult> Rayresults = new List<RaycastResult>();
            m_EventSystem.RaycastAll(pointerdata, Rayresults);
            
            for (int i = 0; i < Rayresults.Count; i++)
            {
                print("Loop");
                if (Rayresults[i].gameObject.TryGetComponent(out TrashButton trashButton))
                {
                    trashButton.PrintData();
                }
            }
        }
    }
}
