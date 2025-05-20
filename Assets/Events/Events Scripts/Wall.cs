using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private GameObject go;
    public PlayerStats stats;
    public GameObject tip;
    private bool recover = false;
    private void Start()
    {
        go = GameObject.FindGameObjectWithTag("Player");
        stats = go.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if(recover&& Input.GetKeyDown(KeyCode.X))                
           stats.currentHP=stats.maxHP.GetValue();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tip.SetActive(true);
            recover = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tip.SetActive(false);
            recover= false;
        }
    }
}
