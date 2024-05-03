using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Coin : MonoBehaviour, Icollectable
{
    [SerializeField] float m_RotationDuration = 2f;
    [SerializeField] Transform m_Tr;
    [SerializeField] int m_CoinValue;
    UIManager m_UIManager;
    //Rigidbody m_RB;
    [SerializeField] bool m_HasCollected = false;
    [SerializeField] AnimationCurve m_AnimationCurve;
    [SerializeField] float m_MoveDuration = 0.5f;
    [SerializeField] float m_xDisplacement = 3;

    SoundManager m_soundManager;
    Tween tw;
    private void Start()
    {
        m_UIManager = UIManager.Instance;
        m_soundManager = SoundManager.Instance;
        //m_RB = GetComponent<Rigidbody>();
        tw = m_Tr.DOLocalRotate(new Vector3(0, 360, 0), m_RotationDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }
    public void MoveCoinToCanvas()
    {
        if (!m_HasCollected)
        {
            Vector3 targetPos = m_UIManager.UiPos();
            //transform.DOMove(targetPos, m_MoveDuration);
            //transform.DOScale(0.5f, m_MoveDuration).OnComplete(() =>
            //{
            //    gameObject.SetActive(false);
            //});
            //m_soundManager.Play("CoinPickup");
            StartCoroutine(MoveWithXOffset(targetPos));
        }
    }
    IEnumerator MoveWithXOffset(Vector3 _Target)
    {
        float timepassed = 0;
        Vector3 endPoint = _Target;
        Vector3 startPos = transform.position;
        while (timepassed < m_MoveDuration)
        {
            timepassed += Time.deltaTime;
            float linearT = timepassed / m_MoveDuration;
            float xOffset = m_AnimationCurve.Evaluate(linearT);
            float xVal = Mathf.Lerp(0, m_xDisplacement, xOffset);
            transform.position = Vector3.Lerp(startPos, endPoint, linearT) + new Vector3(-xVal,0,0);
            print(xVal);
            yield return null;
        }
    }

}
