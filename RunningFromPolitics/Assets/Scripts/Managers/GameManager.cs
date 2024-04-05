using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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


    private bool isGameOver = false;
    private bool hasGameStarted = false;

    //Player Reference
    [SerializeField] GameObject m_Player;
    Animator m_Anim;
    private PlayerMovement m_PlayerMovement;
    private PlayerHealth m_PlayerHealth;
    [SerializeField] CinemachineVirtualCamera m_LookAtCam;
    [SerializeField] GameObject m_StartSceneBackground;
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
        hasGameStarted = false;
        isGameOver = false;
        m_Anim = m_Player.GetComponentInChildren<Animator>();
        m_PlayerMovement = m_Player.GetComponent<PlayerMovement>();
        m_PlayerHealth = m_Player.GetComponent<PlayerHealth>();
        m_PlayerHealth.OnDeath.AddListener(OnDeath);
        m_PlayerStartPosition = m_Player.transform.position;
        m_UIManager = UIManager.Instance;
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
        }
    }
    public void GameStarted()
    {
        hasGameStarted = true;
        m_Anim.SetBool("StartRunning", true);
        m_LookAtCam.Priority = 9;
        m_PlayerMovement.CanMove = true;
        m_StartSceneBackground.SetActive(false);
        m_Anim.ResetTrigger("Start");
        m_UIManager.ResetHealthIcon();
    }

    public void GameOver()
    {
        isGameOver = true;
        m_PlayerMovement.CanMove = false;
        m_Anim.SetBool("StartRunning", false);
        m_Anim.SetTrigger("Death");
        m_PlayerHealth.OnDeath?.Invoke();
    }
}
