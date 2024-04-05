using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : ThrowableItem
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void OnGrab(float _zDistance)
    {
        base.OnGrab(_zDistance);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
