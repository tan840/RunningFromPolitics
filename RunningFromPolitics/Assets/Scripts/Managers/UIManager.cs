using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    ScoreManager m_ScoreManager;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("UIManager");
                    instance = singletonObject.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    public RectTransform CoinTextPos { get => m_CoinTextPos; set => m_CoinTextPos = value; }

    [SerializeField] CanvasGroup m_StartPannel;
    [SerializeField] CanvasGroup m_GamePannel;
    [SerializeField] Button m_StartButton;
    [SerializeField] Image[] m_HealthSprites;
    [SerializeField] RectTransform m_CoinTextPos;
    CanvasGroup m_CurrentCanvas;
    GameManager m_GameManager;
    Camera m_Camera;
    //WaitForSeconds m_ShowPannelDelay = new WaitForSeconds(2);
    //WaitForSeconds m_WaitForSeconds = new WaitForSeconds(4);
    Vector3 m_UIMoveToPosition = Vector3.zero;
    float nearClipPlane = 20;
    private void Start()
    {
        m_Camera = Camera.main;
        m_ScoreManager = ScoreManager.Instance;
        m_CurrentCanvas = m_StartPannel;
        m_GameManager = GameManager.Instance;
        m_StartButton.onClick.AddListener(() => { StartGame(); });
        
    }
    public Vector3 UiPos()
    {
        m_UIMoveToPosition = m_Camera.ScreenToWorldPoint(new Vector3(m_CoinTextPos.position.x, m_CoinTextPos.position.y, m_Camera.nearClipPlane + nearClipPlane));
        return m_UIMoveToPosition;
    }
    void StartGame()
    {
        SwitchCanvasTO(m_GamePannel);
        m_GameManager.GameStarted();
    }
    public void ShowStartPannel()
    {
        SwitchCanvasTO(m_StartPannel);
    }
    public void ResetHealthIcon()
    {
        foreach (var item in m_HealthSprites)
        {
            item.DOFade(1, 0.5f);
        }
    }
    public void OnDamageTaken(int _currentHealth)
    {
        if (_currentHealth < 0) return;

        for (int i = _currentHealth; i < m_HealthSprites.Length; i++)
        {
            m_HealthSprites[i].DOFade(0, 0.5f);
        }
    }
    void SwitchCanvasTO(CanvasGroup _Canvas, float _Time = 1)
    {
        float val1 = m_CurrentCanvas.alpha;
        m_CurrentCanvas.interactable = false;
        m_CurrentCanvas.blocksRaycasts = false;
        DOTween.To(() => val1, x => val1 = x, 0f, _Time)
            .OnUpdate(() =>
            {
                m_CurrentCanvas.alpha = val1;
            })
            .OnComplete(() =>
            {
                m_CurrentCanvas.gameObject.SetActive(false);
                _Canvas.gameObject.SetActive(true);
                float val2 = _Canvas.alpha;
                DOTween.To(() => val2, x => val2 = x, 1f, _Time)
                .OnUpdate(() =>
                {
                    _Canvas.alpha = val2;

                })
                .OnComplete(() =>
                {
                    _Canvas.interactable = true;
                    _Canvas.blocksRaycasts = true;
                    m_CurrentCanvas = _Canvas;
                });
            });
    }
    public Vector3 UICoinWorldPosition()
    {
        Vector3 worldPos = m_Camera.ScreenToWorldPoint(m_CoinTextPos.position);
        return worldPos;
    }
}
