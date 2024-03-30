using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public UnityEvent OnDeath;
    GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameManager.Instance;
    }

    public void TakeHit()
    {
        health--;
        if (health < 1 )
        {
            m_GameManager.GameOver();
            OnDeath?.Invoke();
        }
    }
    public void ResetHealth()
    {
        health = 3;
    }
}
