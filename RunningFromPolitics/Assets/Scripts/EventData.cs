using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;

[CreateAssetMenu(fileName = "EventData", menuName = "EventData/Event", order = 1)]
public class EventData : ScriptableObject
{
    public string NameTag;
    public string HeaderText;
    public int NumberOfItemsToCollect;
    public UnityEvent OnCollect;
    public UnityEvent OnQuestComplete;

}
