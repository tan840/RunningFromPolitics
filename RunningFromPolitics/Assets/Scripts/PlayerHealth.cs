using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public UnityEvent OnDeath;
    GameManager m_GameManager;
    UIManager m_UIManager;

    SoundManager m_SoundManager;

    private void Start()
    {
        m_SoundManager = SoundManager.Instance;
        m_GameManager = GameManager.Instance;
        m_UIManager = UIManager.Instance;
    }

    public void TakeHit()
    {
        health--;
        m_UIManager.OnDamageTaken(health);
        if (health < 1 )
        {
            m_GameManager.GameOver();
            OnDeath?.Invoke();
            m_SoundManager.PlayOnce("Deadsfx");
        }
    }
    public void ResetHealth()
    {
        health = 3;
    }
}
