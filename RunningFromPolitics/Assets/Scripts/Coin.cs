using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour, Icollectable
{
    [SerializeField] float m_RotationDuration = 2f;
    [SerializeField] Transform m_Tr;
    [SerializeField] int m_CoinValue;
    Rigidbody m_RB;


    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Tr.DOLocalRotate(new Vector3(0, 360, 0), m_RotationDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }
    public void Collect()
    {
        //MoveTo UI
        gameObject.SetActive(false);
    }
}
