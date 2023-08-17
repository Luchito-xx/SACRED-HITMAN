using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAleatorio : MonoBehaviour
{
    
    private Rigidbody2D rb2D;
    private SpriteRenderer mySpriteRenderer;
    private float tiempo;

    [SerializeField] private float velocidadMovimiento;

    [SerializeField] private float distancia;

    [SerializeField] private LayerMask queEsSuelo;
    

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        DatosAleatorio();
        InvokeRepeating("Girar", 1f, tiempo);
        InvokeRepeating("VelocidadAleatoria", 1f, 4f);
    }

    void Update()
    {
        rb2D.velocity = new Vector2(velocidadMovimiento * transform.right.x, rb2D.velocity.y);
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distancia);
    }

    private void DatosAleatorio()
    {
        tiempo = Random.Range(3f , 7f);
            
    }

    private void VelocidadAleatoria()
    {
        velocidadMovimiento = Random.Range(3f, 8f);
    }
}
