
using UnityEngine;

public interface IGrabbable
{
    string ItemTag { get; }
    void OnGrab(float zDistance);
    Transform GetTransform();
}
