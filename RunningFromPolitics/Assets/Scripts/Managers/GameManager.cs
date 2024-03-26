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
    [SerializeField] CinemachineVirtualCamera m_LookAtCam;

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
    }

    public void GameStarted()
    {
        hasGameStarted = true;
        m_Anim.SetBool("StartRunning", true);
        m_LookAtCam.Priority = 9;
        m_PlayerMovement.CanMove = true;
    }

    public void GameOver()
    {
        isGameOver = true;
        m_PlayerMovement.CanMove = false;
        m_Anim.SetBool("StartRunning", false);
    }
}
