using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReDash : MonoBehaviour
{
    private PlayerMove player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        // Not really nessecery 
        if (other.CompareTag("Untagged"))
        {
            Debug.Log("Redash");
            player.ReDash();
        }
         
    }

}
