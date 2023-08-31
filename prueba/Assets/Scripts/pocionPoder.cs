using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocionPoder : MonoBehaviour
{
    [SerializeField] private    GameObject  pocionpoder;
    public bool poder = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        pocionpoder.SetActive(false);
        poder = true;
        Debug.Log("poder activo instaa poleentaaaa");
    }

    private void movimiento()
    {
        
    }

    public void activadoPoder()
    {
        poder = true;
    }
}
