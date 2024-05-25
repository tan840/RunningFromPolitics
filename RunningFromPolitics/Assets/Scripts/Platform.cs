using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] List<Coin> m_CoinList = new List<Coin>();
    [SerializeField] Material m_Material;
    //[SerializeField] MeshFilter[] meshFilters;
    private void Start()
    {
        Coin[] coin = GetComponentsInChildren<Coin>();
        foreach (var item in coin)
        {
            m_CoinList.Add(item);
        }
    }
    
    public  IEnumerator DestroyPlatform()
    {
        foreach (var item in m_CoinList)
        {
            item.tw.Kill(false);
            yield return null;
        }
    }
}
