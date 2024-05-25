using UnityEngine;
using System;
using System.Collections;
public class Helper : MonoBehaviour
{
    private static Helper _instance;

    private void Awake()
    {
        // Ensure that there's only one instance of the Helper in the scene
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DelayedCall(Action function, float delay)
    {
        if (_instance == null)
        {
            GameObject helperObject = new GameObject("Helper");
            _instance = helperObject.AddComponent<Helper>();
        }

        _instance.StartCoroutine(_instance.DelayedCallCoroutine(function, delay));
    }

    private IEnumerator DelayedCallCoroutine(Action function, float delay)
    {
        yield return new WaitForSeconds(delay);
        function.Invoke();
    }
}