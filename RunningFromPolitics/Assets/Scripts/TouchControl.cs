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


    SoundManager m_soundManager;
    private void Start()
    {
        m_Camera = Camera.main;
        m_EventSystem = EventSystem.current;
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
            CheckMouseOverButton(m_Grabbable);
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
        if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity, m_LayerMask))
        {
            if (m_Hit.collider.gameObject.TryGetComponent(out IGrabbable GrabItem))
            {
                m_Grabbable = GrabItem;
                m_ZDistance = (m_Grabbable.GetTransform().position - m_Camera.transform.position).z;
                m_soundManager.Play("MindControl");
            }
            //else if (m_Hit.collider.gameObject.TryGetComponent(out Icollectable Coin))
            //{
            //    Coin.Collect();
            //}
        }
    }
    void RaycastConst()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity, m_LayerMask))
        {
            //if (m_Hit.collider.gameObject.TryGetComponent(out IGrabbable GrabItem))
            //{
            //    m_Grabbable = GrabItem;
            //    m_ZDistance = (m_Grabbable.GetTransform().position - m_Camera.transform.position).z;
            //    m_soundManager.Play("MindControl");
            //}
            if (m_Hit.collider.gameObject.TryGetComponent(out Icollectable Coin))
            {
                Coin.Collect();
            }
        }
    }
    void CheckMouseOverTrash()
    {

        TrashButton m_TrashBtn = null;
        if (m_EventSystem.IsPointerOverGameObject())
        {
            PointerEventData pointerdata = new PointerEventData(m_EventSystem);
            pointerdata.position = Input.mousePosition;
            List<RaycastResult> Rayresults = new List<RaycastResult>();
            m_EventSystem.RaycastAll(pointerdata, Rayresults);

            for (int i = 0; i < Rayresults.Count; i++)
            {
                if (Rayresults[i].gameObject.TryGetComponent(out TrashButton trashButton))
                {
                    m_TrashBtn = trashButton;
                    m_TrashBtn.ScaleIcon();
                }
            }
        }
        else
        {
            m_TrashBtn?.ScaleIconReset();
            print(m_TrashBtn);
        }
    }
    void CheckMouseOverButton(IGrabbable _Item)
    {
        if (m_EventSystem.IsPointerOverGameObject())
        {
            PointerEventData pointerdata = new PointerEventData(m_EventSystem);
            pointerdata.position = Input.mousePosition;
            List<RaycastResult> Rayresults = new List<RaycastResult>();
            m_EventSystem.RaycastAll(pointerdata, Rayresults);

            for (int i = 0; i < Rayresults.Count; i++)
            {
                if (Rayresults[i].gameObject.TryGetComponent(out TrashButton trashButton))
                {
                    trashButton.Collect(_Item);
                }
            }
        }
    }
}
