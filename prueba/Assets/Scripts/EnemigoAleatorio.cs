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
    private Animator animator;

    private static int enemigosGenerados = 0;
    private static int maxEnemigos = 2;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        DatosAleatorio();
        InvokeRepeating("Girar", 1f, tiempo);
        InvokeRepeating("VelocidadAleatoria", 1f, 4f);
        animator = GetComponent<Animator>();

        // Verificar si se ha alcanzado el límite de enemigos antes de generar uno nuevo.
        if (enemigosGenerados < maxEnemigos)
        {
            enemigosGenerados++; // Incrementar el contador de enemigos generados.
        }
        else
        {
            // Si se alcanzó el límite, destruir este objeto.
            Destroy(gameObject);
        }
    }

    void Update()
    {
        rb2D.velocity = new Vector2(velocidadMovimiento * transform.right.x, rb2D.velocity.y);
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDestroy()
    {
        // Decrementar el contador de enemigos cuando este enemigo es destruido.
        enemigosGenerados--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distancia);
    }

    private void DatosAleatorio()
    {
        tiempo = Random.Range(6f, 7f);
    }

    private void VelocidadAleatoria()
    {
        velocidadMovimiento = Random.Range(3f, 8f);
    }
}
