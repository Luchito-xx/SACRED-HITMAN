using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorVictoria : MonoBehaviour
{
    [SerializeField] public GameObject activadorVictoria;

    [SerializeField] public GameObject panel;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
