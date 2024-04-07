using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] TMP_Text m_ScoreText;
    string HIGH_SCORE;

    //ToDo= Reset Score On death  
    private void Start()
    {
        m_MaxScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
        ScoreTextUpdate(m_CurrentScore);
    }

    public void IncrimentScore(int _score)
    {
        m_CurrentScore += _score;
        ScoreTextUpdate(m_CurrentScore);
    }
    void ScoreTextUpdate(int _Score)
    {
        m_ScoreText.text = "Score: " + _Score.ToString();
    }
    public void Penalty(int _Penalty)
    {
        if (m_CurrentScore <= 0) return;
        m_CurrentScore -= _Penalty;
        if (m_CurrentScore <= 0) m_CurrentScore = 0;
        ScoreTextUpdate(m_CurrentScore);
    }
    public void SetMaxScore()
    {
        if (m_CurrentScore > MaxScore)
        {
            PlayerPrefs.SetString(HIGH_SCORE, m_CurrentScore.ToString());
        }
    }
}
