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
    ScoreManager m_ScoreManager;
    //Rigidbody m_RB;
    [SerializeField] bool m_HasCollected = false;
    [SerializeField] AnimationCurve m_AnimationCurve;
    [SerializeField] float m_MoveDuration = 0.5f;
    [SerializeField] float m_xDisplacement = 3;
    
    /// <summary>
    /// SoundManager m_soundManager;
    /// </summary>
    public Tween tw;
    private void Start()
    {
        m_UIManager = UIManager.Instance;
        m_ScoreManager = ScoreManager.Instance;
        //m_soundManager = SoundManager.Instance;
        //m_RB = GetComponent<Rigidbody>();
        tw = m_Tr.DOLocalRotate(new Vector3(0, 360, 0), m_RotationDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }
    public void MoveCoinToCanvas()
    {
        if (!m_HasCollected)
        {
            m_HasCollected = true;
            Vector3 targetPos = m_UIManager.UiPos();
            StartCoroutine(MoveWithXOffset(targetPos));
        }
    }
    public void KillTween()
    {
        transform.DOKill(false);
    }
    IEnumerator MoveWithXOffset(Vector3 _Target)
    {
        float timepassed = 0;
        tw.Kill();
        Vector3 endPoint = _Target;
        Vector3 startPos = m_Tr.position;
        m_Tr.DOLocalRotate(new Vector3(-90, 0, 0), 0.15f);
        m_Tr.DOScale(0.5f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        { 
            gameObject.SetActive(false);
            print("CoinAdded");
            m_ScoreManager.IncrimentScore(1);
        });
        while (timepassed < m_MoveDuration)
        {
            timepassed += Time.deltaTime;
            float linearT = timepassed / m_MoveDuration;
            float xOffset = m_AnimationCurve.Evaluate(linearT);
            float xVal = Mathf.Lerp(0, m_xDisplacement, xOffset);
            m_Tr.position = Vector3.Lerp(startPos, endPoint, linearT) + new Vector3(-xVal,0,0);
            yield return null;
        }
    }

}
