using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour


{
    // public bool canAttack;
    private GM gm;
    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").gameObject.GetComponent<GM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            gm.TakeDamage(20);
        }
        

    }

}
