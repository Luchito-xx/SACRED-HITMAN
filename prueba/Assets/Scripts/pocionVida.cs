using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocionVida : MonoBehaviour
{
    [SerializeField] private GameObject pocionvida;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        pocionvida.SetActive(false);
    }
}