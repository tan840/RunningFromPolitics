using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum GameMode
{
    INFINITE = 0,
    LEVEL_BASED = 1,
}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public GameMode GameMode { get => gameMode; }

    [SerializeField] GameMode gameMode;


    //Player Reference
    [SerializeField] GameObject m_Player;
    [SerializeField] float m_SpeedIncriment = 5;
    Animator m_Anim;
    private PlayerMovement m_PlayerMovement;
    private PlayerHealth m_PlayerHealth;
    private ScoreManager m_ScoreManager;
    private LevelPoolManager m_LevelManager;
    [SerializeField] CinemachineVirtualCamera m_LookAtCam;
    [SerializeField] GameObject m_StartSceneBackground;
    [SerializeField] ParticleSystem[] m_Conffetti;
    Vector3 m_PlayerStartPosition;
    WaitForSeconds m_WaitForSeconds = new WaitForSeconds(4);

    UIManager m_UIManager;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        //hasGameStarted = false;
        //isGameOver = false;
        m_Anim = m_Player.GetComponentInChildren<Animator>();
        m_PlayerMovement = m_Player.GetComponent<PlayerMovement>();
        m_PlayerHealth = m_Player.GetComponent<PlayerHealth>();
        m_PlayerHealth.OnDeath.AddListener(OnDeath);
        m_PlayerStartPosition = m_Player.transform.position;
        m_UIManager = UIManager.Instance;
        m_ScoreManager = ScoreManager.Instance;
        m_LevelManager = LevelPoolManager.Instance;
    }
    public void IncreasePlayerSpeed()
    {
        m_PlayerMovement.MoveSpeed += m_SpeedIncriment;
        if (m_PlayerMovement.MoveSpeed > 1000)
        {
            m_PlayerMovement.MoveSpeed = 1000;
        }
    }
    public void PLayConfetti()
    {
        foreach (ParticleSystem m in m_Conffetti)
        {
            m.Play();
        }
    }
    void OnDeath()
    {
        m_StartSceneBackground.SetActive(true);
        StartCoroutine(DeathSequence());
        IEnumerator DeathSequence()
        {
            yield return m_WaitForSeconds;
            m_Anim.ResetTrigger("Death");
            m_Anim.SetTrigger("Start");
            m_Player.transform.position = m_PlayerStartPosition;
            m_UIManager.ShowStartPannel();
            m_LookAtCam.Priority = 11;
            m_PlayerHealth.ResetHealth();
            m_ScoreManager.SetMaxScore();
            m_LevelManager.ResetPlatform();
        }
    }
    public void GameStarted()
    {
        //hasGameStarted = true;
        m_Anim.SetBool("StartRunning", true);
        m_LookAtCam.Priority = 9;
        m_PlayerMovement.CanMove = true;
        m_StartSceneBackground.SetActive(false);
        m_Anim.ResetTrigger("Start");
        m_UIManager.ResetHealthIcon();
    }

    public void GameOver()
    {
        //isGameOver = true;
        m_PlayerMovement.CanMove = false;
        m_Anim.SetBool("StartRunning", false);
        m_Anim.SetTrigger("Death");
        m_PlayerHealth.OnDeath?.Invoke();
    }
}
