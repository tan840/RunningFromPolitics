using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour, Icollectable
{
    [SerializeField] float m_RotationDuration = 2f;
    [SerializeField] Transform m_Tr;
    [SerializeField] int m_CoinValue;
    UIManager m_UIManager;
    //Rigidbody m_RB;
    [SerializeField] bool m_HasCollected = false;


    SoundManager m_soundManager;
    Tween tw;
    private void Start()
    {
        m_UIManager = UIManager.Instance;
        m_soundManager = SoundManager.Instance;
        //m_RB = GetComponent<Rigidbody>();
        tw = m_Tr.DOLocalRotate(new Vector3(0, 360, 0), m_RotationDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }
    public void Collect()
    {
        MoveToCanvas(m_UIManager.CoinTextPos, 20);
    }
    RectTransform m_CanvasTarget;
    public void MoveToCanvas(RectTransform rect, int nearClipPlane)
    {
        m_CanvasTarget = rect;
        Camera cam = Camera.main;
        if (!m_HasCollected)
        {
            Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(m_CanvasTarget.position.x, m_CanvasTarget.position.y, cam.nearClipPlane + nearClipPlane));
            transform.DOMove(targetPos, 0.5f);
            transform.DOScale(0.5f, 0.4f).OnComplete(() => {
                gameObject.SetActive(false);
            }) ;
            m_soundManager.Play("CoinPickup");
        }
    }
}
