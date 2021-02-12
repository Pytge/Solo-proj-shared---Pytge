using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public bool spawnerOn;

    private void Start()
    {
        StartCoroutine("StartDelay");
        spawnerOn = true;
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine("SpawnLoop");

    }

    public IEnumerator SpawnLoop()
    {
        
        yield return new WaitForSeconds(5f);
        Instantiate(enemy, this.transform.position, this.transform.rotation);
        if (spawnerOn)
        {
            StartCoroutine("SpawnLoop");
        }
        

    }

}
