using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
   [SerializeField] GameObject[] Object;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] GameObject Parent;
    //vector 3... called next spawn point starting 0. add current spawn point to it and use it for next position
    [SerializeField] Vector3 InitalAP;
    [SerializeField] float current_add;

    [SerializeField] float old_value;

    LevelPoolManager m_levelPoolManager;
    private void Start()
    {
        m_levelPoolManager = LevelPoolManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            m_levelPoolManager.LoadNextPlatform();
            //print("Spawnned");
        }
    }
}
