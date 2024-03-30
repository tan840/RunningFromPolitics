
public interface IGrabbable
{
    string ItemTag { get; }
    void OnGrab(float zDistance);
}
