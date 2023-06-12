using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemigo : MonoBehaviour
{   
    [SerializeField] private float vida;

    private Animator animator;

    public GameObject angelDesactivado;
    
    private bool cooldown = true;

    public Collider2D angelCLD;

    public AudioSource muerte;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        if(vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        if (cooldown)
        {
            muerte.Play();
            animator.SetTrigger("Muerte");
            cooldown = false;
        } if (!cooldown)
        {
            angelDesactivado.SetActive(true);
        }
        
    }
}
