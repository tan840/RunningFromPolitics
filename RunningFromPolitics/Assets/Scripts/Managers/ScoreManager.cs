using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("ScoreManager");
                    instance = singletonObject.AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }
    //hasgrab sound played
    public int MaxScore { get => m_MaxScore; set => m_MaxScore = value; }

    [SerializeField] int m_CurrentScore;
    [SerializeField] int m_MaxScore;
    string HIGH_SCORE;

    //ToDo= Reset Score On death  
    private void Start()
    {
        MaxScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }

    public void IncrimentScore(int _score)
    {
        m_CurrentScore += _score;
    }
    public void Penalty(int _Penalty)
    {
        if (m_CurrentScore <= 0) return;
        m_CurrentScore -= _Penalty;
        if (m_CurrentScore <= 0) m_CurrentScore = 0;
    }
    public void SetMaxScore()
    {
        if (m_CurrentScore > MaxScore)
        {
            PlayerPrefs.SetString(HIGH_SCORE, m_CurrentScore.ToString());
        }
    }
}
