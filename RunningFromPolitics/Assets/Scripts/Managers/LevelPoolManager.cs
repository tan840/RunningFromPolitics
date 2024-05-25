using Cinemachine;
using RootMotion.Dynamics;
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


    [SerializeField] int m_PlatformLength = 40;
    [Space(50)]
    [SerializeField] private List<LevelData> m_LevelPlatforms;
    Vector3 m_SpawnPosition = Vector3.zero;
    GameManager m_Gamemanager;
    [SerializeField] List<Platform> m_SpawnedPlatforms;

    private void Start()
    {
        m_Gamemanager = GameManager.Instance;
        SpawnPlatform(m_Gamemanager.CurrentLevel);
    }
    public void SpawnPlatform(int _CurrentLevel)
    {
        
        if (_CurrentLevel >= m_LevelPlatforms.Count)
        {
            m_Gamemanager.CurrentLevel = 0;
            _CurrentLevel = 0;      
        }
        StartCoroutine(GeneratePlatform());
        IEnumerator GeneratePlatform()
        {
            if (m_LevelPlatforms[_CurrentLevel] != null)
            {
                for (int index = 0; index < m_SpawnedPlatforms.Count ; index++)
                {
                    if (m_SpawnedPlatforms[index] != null)
                    {
                        yield return m_SpawnedPlatforms[index].DestroyPlatform();
                        yield return null;
                        m_SpawnedPlatforms[index]?.StopAllCoroutines();
                        Destroy(m_SpawnedPlatforms[index].gameObject);
                    }
                }
                m_SpawnedPlatforms.Clear();
                m_SpawnPosition.z = 0;
                yield return null;
                for (int j = 0; j < m_LevelPlatforms[_CurrentLevel].LevelPlatforms.Length; j++)
                {                 
                    m_SpawnPosition.z += m_PlatformLength;
                    Platform platform = Instantiate(m_LevelPlatforms[_CurrentLevel].LevelPlatforms[j]);
                    platform.transform.position = m_SpawnPosition;
                    m_SpawnedPlatforms.Add(platform);
                }
            }
        }
    }
}
