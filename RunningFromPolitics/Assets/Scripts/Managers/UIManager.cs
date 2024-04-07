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

    [SerializeField] CanvasGroup m_StartPannel;
    [SerializeField] CanvasGroup m_GamePannel;
    [SerializeField] CanvasGroup m_TutorialPannel;
    [SerializeField] Button m_StartButton;
    [SerializeField] Image[] m_HealthSprites;
    CanvasGroup m_CurrentCanvas;
    GameManager m_GameManager;
    WaitForSeconds m_ShowPannelDelay = new WaitForSeconds(2);
    WaitForSeconds m_WaitForSeconds = new WaitForSeconds(4);
    private void Start()
    {
        m_ScoreManager = ScoreManager.Instance;
        m_CurrentCanvas = m_StartPannel;
        m_GameManager = GameManager.Instance;
        m_StartButton.onClick.AddListener(() => { StartGame(); });
    }
    void StartGame()
    {
        SwitchCanvasTO(m_GamePannel);
        m_GameManager.GameStarted();
        StartCoroutine(TutorialPannel());
    }
    IEnumerator TutorialPannel()
    {
        yield return m_ShowPannelDelay;
        float val1 = m_TutorialPannel.alpha;
        DOTween.To(() => val1, x => val1 = x, 1f, 0.5f)
            .OnUpdate(() =>
            {
                m_TutorialPannel.alpha = val1;
            });
        yield return m_WaitForSeconds;
        float val2 = m_TutorialPannel.alpha;
        DOTween.To(() => val2, x => val2 = x, 0f, 0.25f)
        .OnUpdate(() =>
         {
             m_TutorialPannel.alpha = val2;
         }).OnComplete(() => {
             m_TutorialPannel.gameObject.SetActive(false);
         });
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
}
