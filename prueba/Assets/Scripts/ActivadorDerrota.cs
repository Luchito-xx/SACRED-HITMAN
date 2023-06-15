using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDerrota : MonoBehaviour
{
    [SerializeField] public GameObject activadorDerrota;

    [SerializeField] public GameObject panelDerrota;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            panelDerrota.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
