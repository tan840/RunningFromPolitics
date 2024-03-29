using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoolManager : MonoBehaviour
{
    private static LevelPoolManager instance;
    public static LevelPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelPoolManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("LevelPoolManager");
                    instance = singletonObject.AddComponent<LevelPoolManager>();
                }
            }
            return instance;
        }
    }

    public List<GameObject> Obstacle { get => m_Obstacle; }

    [SerializeField] private List<Platform> m_PlatformList;
    [SerializeField] private List<GameObject> m_Obstacle;
    [SerializeField] float m_PlatformLength = 100;
    int m_CurrentPlatformIndex = -1;
    Vector3 m_SpawnPosition = Vector3.zero;

    public void LoadNextPlatform()
    {
        int index = GetNextPlatformIndex();
        
        m_SpawnPosition.z += m_PlatformLength;
        m_PlatformList[index].transform.position = m_SpawnPosition;
        m_PlatformList[index].InitializeObstacle();
    }
    int GetNextPlatformIndex()
    {
        if (m_PlatformList != null)
        {
            //print(m_PlatformList.Count + " platform");
            if (m_CurrentPlatformIndex >= m_PlatformList.Count -1)
            {
                m_CurrentPlatformIndex = -1;
            }
            m_CurrentPlatformIndex++;
            //print(m_CurrentPlatformIndex + " m_CurrentPlatformIndex");
            return m_CurrentPlatformIndex;
        }
        return 0;
    }
}
