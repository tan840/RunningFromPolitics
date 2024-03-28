using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    [SerializeField] GameObject[] Object;
    // public Collider TriggerCol;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] GameObject Parent;
    //vector 3... called next spawn point starting 0. add current spawn point to it and use it for next position
    [SerializeField] Vector3 InitalAP;
    [SerializeField] float current_add;

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            var e = Instantiate(Object[0], this.transform.position, Quaternion.identity) as GameObject;
            e.transform.position = new Vector3(0,0,InitalAP.z + current_add);
            current_add += 100;
            
            print(current_add);
        }
    }
}
