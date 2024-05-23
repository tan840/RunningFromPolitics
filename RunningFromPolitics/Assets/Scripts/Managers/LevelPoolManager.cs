using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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


    [SerializeField] float m_PlatformLength = 100;

    [Space(50)]
    [SerializeField] private List<LevelData> m_LevelPlatforms;
    Vector3 m_SpawnPosition = Vector3.zero;
    GameManager m_Gamemanager;

    private void Start()
    {
        m_Gamemanager = GameManager.Instance;
        SpawnPlatform(m_Gamemanager.CurrentLevel);
    }
    void SpawnPlatform(int _CurrentLevel)
    {
        if (_CurrentLevel >= m_LevelPlatforms.Count)
        {
            m_Gamemanager.CurrentLevel = 0;
            _CurrentLevel = 0;
        }
        if (m_LevelPlatforms[_CurrentLevel] != null)
        {
            for (int j = 0; j < m_LevelPlatforms[_CurrentLevel].LevelPlatforms.Length; j++)
            {
                m_SpawnPosition.z += m_PlatformLength;
                Platform platform = Instantiate(m_LevelPlatforms[_CurrentLevel].LevelPlatforms[j]);
                platform.transform.position = m_SpawnPosition;
            }
        }
    }
    //public void ResetPlatform()
    //{
    //    m_SpawnPosition = Vector3.zero;
    //    m_PlatformLength = 100;
    //}
}
