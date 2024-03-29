using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] m_Spawnpoints;
    LevelPoolManager m_levelPoolManager;
    List<GameObject> m_Obstacle;
    private void Start()
    {
        m_levelPoolManager = LevelPoolManager.Instance;
        InstantiateObstacle();
    }
    void InstantiateObstacle()
    {
        m_Obstacle = new List<GameObject>();
        foreach (var point in m_Spawnpoints) 
        {
            int index = Random.Range(0, m_levelPoolManager.Obstacle.Count);
            GameObject obstacle = Instantiate(m_levelPoolManager.Obstacle[index], 
                point.position, Quaternion.Euler(point.rotation.x, Random.Range(0, 360),
                point.rotation.z), point);
            m_Obstacle.Add(obstacle);
        }
    }
  
    public void InitializeObstacle()
    {
        ResetObstacle();
    }
    void ResetObstacle()
    {
        foreach (var obstacle in m_Obstacle)
        {
            obstacle.transform.localPosition = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            m_levelPoolManager.LoadNextPlatform();
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (m_Spawnpoints.Length > 0)
        {
            Gizmos.color = Color.blue;
            foreach (Transform point in m_Spawnpoints)
            {
                Gizmos.DrawCube(point.position, Vector3.one);
            }
        }
    }

#endif
}
