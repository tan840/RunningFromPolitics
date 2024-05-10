using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RootMotion.Dynamics;
using static RootMotion.Demos.CharacterMeleeDemo.Action;
using DG.Tweening;

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
    public CinemachineVirtualCamera LevelEndCam { get => m_LevelEndCam; set => m_LevelEndCam = value; }
    public CinemachineBrain BrainCam { get => m_BrainCam; set => m_BrainCam = value; }
    public int CurrentLevel { get => m_CurrentLevel; set => m_CurrentLevel = value; }

    [SerializeField] GameMode gameMode;


    //Player Reference
    [SerializeField] GameObject m_Player;
    [SerializeField] float m_SpeedIncriment = 5;
    Animator m_Anim;
    private PlayerMovement m_PlayerMovement;
    private PlayerHealth m_PlayerHealth;
    private ScoreManager m_ScoreManager;
    private LevelPoolManager m_LevelManager;
    [SerializeField] CinemachineBrain m_BrainCam;
    [SerializeField] CinemachineVirtualCamera m_LookAtCam;
    [SerializeField] CinemachineVirtualCamera m_LevelEndCam;
    [SerializeField] GameObject m_StartSceneBackground;
    [SerializeField] int m_CurrentLevel = 0;
    string CURRENT_LEVEL = "CURRENT_LEVEL";

    PuppetMaster m_PuppetMaster;
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
        }
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        m_Anim = m_Player.GetComponentInChildren<Animator>();
        m_PlayerMovement = m_Player.GetComponent<PlayerMovement>();
        m_PlayerHealth = m_Player.GetComponent<PlayerHealth>();
        m_PlayerHealth.OnDeath.AddListener(OnDeath);
        m_PlayerStartPosition = m_Player.transform.position;
        m_UIManager = UIManager.Instance;
        m_ScoreManager = ScoreManager.Instance;
        m_LevelManager = LevelPoolManager.Instance;
        m_PuppetMaster = m_Player.GetComponentInChildren<PuppetMaster>();
        m_BrainCam.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        m_CurrentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL, 0);
    }
    public void IncreasePlayerSpeed()
    {
        m_PlayerMovement.MoveSpeed += m_SpeedIncriment;
        if (m_PlayerMovement.MoveSpeed > 1000)
        {
            m_PlayerMovement.MoveSpeed = 1000;
        }
    }

    public void OnLevelComplete()
    {
        m_Anim.ResetTrigger("Start");
        m_Anim.transform.DORotate(new Vector3(0, -180, 0), 0.25f).SetEase(Ease.OutCubic);
        m_PlayerMovement.CanMove = false;
        m_PlayerMovement.RB.isKinematic = true;
        int Rand = Random.Range(1, 3);
        m_Anim.SetInteger("Victory", Rand);
        m_CurrentLevel++;
        PlayerPrefs.SetInt(CURRENT_LEVEL,m_CurrentLevel);
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
            m_PlayerMovement.RB.isKinematic = false;
            m_PuppetMaster.state = PuppetMaster.State.Alive;
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
        m_Anim.SetBool("StartRunning", true);
        m_LookAtCam.Priority = 9;
        m_PlayerMovement.CanMove = true;
        m_StartSceneBackground.SetActive(false);
        m_Anim.ResetTrigger("Start");
        m_UIManager.ResetHealthIcon();
    }

    public void GameOver()
    {
        m_PlayerMovement.CanMove = false;
        m_Anim.SetBool("StartRunning", false);
        m_PlayerMovement.RB.isKinematic = true;
        m_Anim.SetTrigger("Death");
        m_PuppetMaster.state = PuppetMaster.State.Dead;
        m_PlayerHealth.OnDeath?.Invoke();
        PlayerPrefs.SetInt(CURRENT_LEVEL, m_CurrentLevel);
    }
}
