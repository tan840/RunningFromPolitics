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

    //public List<GameObject> Obstacle { get => m_Obstacle; }

    //[SerializeField] private List<Platform> m_PlatformList;
    //[SerializeField] private List<GameObject> m_Obstacle;
    [SerializeField] float m_PlatformLength = 100;

    [Space(50)]
    [SerializeField] private List<LevelData> m_LevelPlatforms;
    int m_CurrentPlatformIndex = -1;
    Vector3 m_SpawnPosition = Vector3.zero;
    GameManager m_Gamemanager;

    private void Start()
    {
        m_Gamemanager = GameManager.Instance;
        switch (m_Gamemanager.GameMode)
        {
            case GameMode.INFINITE:
                break;
            case GameMode.LEVEL_BASED:
                SpawnPlatform();
                break;
            default:
                break;
        }
    }
    void SpawnPlatform()
    {
        foreach (var item in m_LevelPlatforms)
        {
            if (item != null)
            {
                //m_SpawnPosition.z += m_PlatformLength;
                for (int i = 0; i < item.LevelPlatforms.Length; i++)
                {
                    m_SpawnPosition.z += m_PlatformLength;
                    Platform platform = Instantiate(item.LevelPlatforms[i]);
                    platform.transform.position = m_SpawnPosition;
                }
            }
        }
    }
    public void ResetPlatform()
    {
        m_CurrentPlatformIndex = -1;
        m_SpawnPosition = Vector3.zero;
        m_PlatformLength = 100;
    }
    public void LoadNextPlatform()
    {
        int index = GetNextPlatformIndex();
        
        m_SpawnPosition.z += m_PlatformLength;
        //m_PlatformList[index].transform.position = m_SpawnPosition;
        //m_PlatformList[index].InitializeObstacle();
    }
    int GetNextPlatformIndex()
    {
        //if (m_PlatformList != null)
        //{
        //    if (m_CurrentPlatformIndex >= m_PlatformList.Count -1)
        //    {
        //        m_CurrentPlatformIndex = -1;
        //    }
        //    m_CurrentPlatformIndex++;
        //    return m_CurrentPlatformIndex;
        //}
        return 0;
    }
}
