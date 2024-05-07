using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    bool hasCollided = false;
    GameManager m_GameManager;
    [SerializeField] ParticleSystem[] m_Conffetti;
    private void Start()
    {
        m_GameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided && other.gameObject.layer == 7)
        {
            hasCollided = true;
            m_GameManager.OnLevelComplete();
        }
    }
}