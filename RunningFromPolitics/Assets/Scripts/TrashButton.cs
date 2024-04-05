using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashButton : MonoBehaviour
{
    [SerializeField] EventData[] m_Events;
    [SerializeField] TMP_Text m_HeaderText;
    [SerializeField] int m_CurrentEventIndex = 0;
    int CollectedItemCount = 0;
    EventData m_CurrentEvent;
    private void Start()
    {
        if (m_Events != null)
        {
            m_CurrentEvent = m_Events[0];
            m_CurrentEvent.OnCollect.AddListener(CollectedItem);
            m_CurrentEvent.OnQuestComplete.AddListener(QuestComplted);
            m_HeaderText.text = m_CurrentEvent.HeaderText;
        }
    }
    void CollectedItem()
    {
        
        CollectedItemCount++;
        if (m_CurrentEvent.NumberOfItemsToCollect <= CollectedItemCount)
        {
            m_CurrentEvent.OnQuestComplete?.Invoke();
        }
    }
    void OnDamageTaken()
    {

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
        if (_Item.ItemTag == m_CurrentEvent.NameTag)
        {
            m_CurrentEvent.OnCollect?.Invoke();
            print("ItemCollected " + _Item.ItemTag);
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log("Item Does Not Match");
        }
#endif
    }
}
