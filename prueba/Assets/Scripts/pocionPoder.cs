using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocionPoder : MonoBehaviour
{
    [SerializeField] private    GameObject  pocionpoder;
    //private bool cooldown = true;

  
    void OnTriggerEnter2D (Collider2D col)
    {
        Bandit bandit = col.gameObject.GetComponent<Bandit>();
        if(bandit){
            bandit.ActivarPoder();  
            pocionpoder.SetActive(false);
        }
    }

    /*private void FixedUpdate()
    {
        Bandit bandit = gameObject.GetComponent<Bandit>();
        if (!cooldown){
            bandit.SetearPoder();
        }
    }*/
}

