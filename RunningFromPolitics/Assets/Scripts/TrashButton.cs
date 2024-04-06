using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashButton : MonoBehaviour
{
    [SerializeField] EventData[] m_Events;
    [SerializeField] Text m_HeaderText;
    [SerializeField] int m_CurrentEventIndex = 0;
    [SerializeField] RectTransform m_RectTransform;
    int CollectedItemCount = 0;
    EventData m_CurrentEvent;
    ScoreManager m_ScoreManager;
    bool isScaled = false;
    private void Start()
    {
        if (m_Events != null)
        {
            m_CurrentEvent = m_Events[0];
            m_CurrentEvent.OnCollect.AddListener(CollectedItem);
            m_CurrentEvent.OnQuestComplete.AddListener(QuestComplted);
            m_HeaderText.text = m_CurrentEvent.HeaderText;
        }
        m_ScoreManager = ScoreManager.Instance;
        m_RectTransform = GetComponent<RectTransform>();
    }
    void CollectedItem()
    {

        CollectedItemCount++;
        if (m_CurrentEvent.NumberOfItemsToCollect <= CollectedItemCount)
        {
            m_CurrentEvent.OnQuestComplete?.Invoke();
        }
        ScaleIconReset();

    }
    public void ScaleIcon()
    {
        if (isScaled == false)
        {
            isScaled = true;
            m_RectTransform.DOScale(1.5f, 0.25f);
        }
    }
    public void ScaleIconReset()
    {
        m_RectTransform.DOScale(1f, 0.25f);
        isScaled = false;
    }
    void QuestComplted()
    {
        print("QuestComplted");
        m_CurrentEvent.OnCollect.RemoveListener(CollectedItem);
        m_CurrentEvent.OnQuestComplete.RemoveListener(QuestComplted);
        CollectedItemCount = 0;
        m_CurrentEventIndex++;
        if (m_Events.Length > m_CurrentEventIndex)
        {
            m_CurrentEvent = m_Events[m_CurrentEventIndex];
            m_CurrentEvent.OnCollect.AddListener(CollectedItem);
            m_CurrentEvent.OnQuestComplete.AddListener(QuestComplted);
            m_HeaderText.text = m_CurrentEvent.HeaderText;
        }
    }
    public void Collect(IGrabbable _Item)
    {
        if (_Item == null) return;

        if (_Item.ItemTag == m_CurrentEvent.NameTag)
        {
            m_CurrentEvent.OnCollect?.Invoke();
            //print("ItemCollected " + _Item.ItemTag);
            m_ScoreManager.IncrimentScore(10);
        }
#if UNITY_EDITOR
        else
        {
            //Debug.Log("Item Does Not Match");
        }
#endif
    }
}
