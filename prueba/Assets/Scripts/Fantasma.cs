using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{

    [SerializeField] private float velocidadFantasma;
    
    [SerializeField] private Transform[] puntosMovimientoFantasma;

    [SerializeField] private float distanciaMinima;

    private int siguientePunto = 0;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientoFantasma[siguientePunto].position, velocidadFantasma * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimientoFantasma[siguientePunto].position) < distanciaMinima)
        {
            siguientePunto += 1;
            if (siguientePunto >= puntosMovimientoFantasma.Length)
            {
                siguientePunto = 0;
            }
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimientoFantasma[siguientePunto].position.x)
        {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = true;
        }
    }

}
