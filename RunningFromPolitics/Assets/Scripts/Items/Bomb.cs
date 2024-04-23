using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : ThrowableItem
{
    ParticleSystem m_Explosion;
    public override void Awake()
    {
        base.Awake();
        m_Explosion = GetComponentInChildren<ParticleSystem>();
    }
    public override void OnGrab(float _zDistance)
    {
        base.OnGrab(_zDistance);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        if (!hasCollided && collision.gameObject.layer == 7 && collision.collider.TryGetComponent(out PlayerHealth health))
        {
            hasCollided = true;
            health.killPlayer();
            m_Explosion?.Play();
            m_Explosion.transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
}
