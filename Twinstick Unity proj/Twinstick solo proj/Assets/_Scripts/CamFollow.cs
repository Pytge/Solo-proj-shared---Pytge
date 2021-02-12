using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    private Transform playerBody;
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        this.transform.position = new Vector3(playerBody.position.x, this.transform.position.y, playerBody.position.z-(5f));
    }
}
