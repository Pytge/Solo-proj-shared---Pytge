using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    private RectTransform healthFill;
    public float health =100f;

    private void Awake()
    {
        healthFill = GameObject.Find("HP_Fill").GetComponent<RectTransform>();
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage");
        health -= damage;
        HealthUpdate();
    }

    public void HealthUpdate()
    {
        
        healthFill.sizeDelta = new Vector2((health*4f), 100f);
    }



}
