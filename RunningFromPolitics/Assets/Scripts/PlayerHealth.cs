using RootMotion.Dynamics;
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
    PuppetMaster m_PuppetMaster;
    SoundManager m_SoundManager;

    private void Start()
    {
        m_SoundManager = SoundManager.Instance;
        m_GameManager = GameManager.Instance;
        m_UIManager = UIManager.Instance;
        m_PuppetMaster = GetComponentInChildren<PuppetMaster>();
    }

    public void TakeHit()
    {
        m_SoundManager.PlayOnce("Hurt");
        health--;
        m_UIManager.OnDamageTaken(health);
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

    public void killPlayer()
    {
        health -= health;
        m_UIManager.OnDamageTaken(health);
        if (health < 1)
        {
            m_GameManager.GameOver();
            //OnDeath?.Invoke();
            m_SoundManager.PlayOnce("Deadsfx");
            //print(m_PuppetMaster.muscles[0].name);
            m_PuppetMaster.muscles[0].rigidbody.AddExplosionForce(600, Vector3.up, 500, 100, ForceMode.Impulse);
        }
    }

}
