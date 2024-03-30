using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject m_ItemTothow;
    [SerializeField] GameObject m_Destinatiion;
    [SerializeField] float m_Duration;
    [SerializeField] Vector3 m_LandPosition;
    ThrowableItem m_Item;
    void Start()
    {
        m_Item = m_ItemTothow.GetComponent<ThrowableItem>();
        //Invoke(nameof(Throw), 2);
    }
    void Throw()
    {
        m_ItemTothow.transform.DOJump(m_Destinatiion.transform.position, 10, 1, m_Duration);
    }

}
