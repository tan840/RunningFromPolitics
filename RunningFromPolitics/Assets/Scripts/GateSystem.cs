using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GateSystem : MonoBehaviour
{
    [SerializeField] Transform m_Gate;
    [SerializeField] float m_MoveDistance;
    [SerializeField] float m_MoveTime;
    [SerializeField] Ease m_Ease;
    [SerializeField] ParticleSystem[] m_Conffetti;
    GameManager m_GameManager;
    bool hasCollided = false;
    private void Start()
    {
        m_GameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided && other.gameObject.layer == 7)
        {
            m_GameManager.BrainCam.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1.75f);
            hasCollided = true;
            m_Gate.transform.DOMoveX(m_MoveDistance, m_MoveTime).SetEase(m_Ease);
            m_GameManager.LevelEndCam.Priority = 15;
            PLayConfetti();
        }
    }
    public void PLayConfetti()
    {
        foreach (ParticleSystem m in m_Conffetti)
        {
            m.Play();
        }
    }
}
