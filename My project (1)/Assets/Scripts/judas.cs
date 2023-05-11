using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judas : MonoBehaviour
{
    //Fuerza con la que se mueve el jugador
    private float movimientoFuerza = 7f;
    private float fuerza = 7f;
    private Rigidbody2D rbd;
    private bool saltarSN;

        // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal"); //esta variable puede ser -1, 0 o 1
        Vector2 posicionJug = transform.position;

        posicionJug = posicionJug + new Vector2(movementX, 0f) * movimientoFuerza * Time.deltaTime;

        transform.position = posicionJug;


        //pa que salte
        if (Input.GetKeyDown(KeyCode.Space) && saltarSN)
        {
            rbd.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "elevator")
        {
            transform.parent = coll.gameObject.transform;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "elevator")
        {
            transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Piso piso = col.GetComponent < Piso > ();
        if (piso)
        {
            saltarSN = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Piso piso = col.GetComponent < Piso > ();
        if (piso)
        {
            saltarSN = false;
        }
    }

    //Parametros movimiento celular
    bool enizquierda = false;
    bool enderecha = false;
    bool enSaltar = false;
    bool puedeSaltar = true;

    
    public float fuerzaVelocidad;
    public float fuerzaSalto;
    public float esperaSaltar;

    public void clickIzquierda()
    {
        enizquierda = true;
    }

    public void releaseIzquierda()
    {
        enizquierda = false;
    }

    public void clickDerecha()
    {
        enderecha = true;
    }

    public void releaseDerecha()
    {
        enderecha = false;
    }
    public void clickSaltar()
    {
        enSaltar = true;
    }

    private void FixedUpdate()
    {
        if (enizquierda)
        {
            rbd.AddForce(new Vector2(-fuerzaVelocidad, 0)* Time.deltaTime);
        }

        if (enderecha)
        {
            rbd.AddForce(new Vector2(fuerzaVelocidad, 0)* Time.deltaTime);
        }

        if (enSaltar && puedeSaltar)
        {
            enSaltar = false;
            rbd.AddForce(new Vector2(0, fuerzaSalto));
            puedeSaltar = false;
            Invoke("esperarSaltar", esperaSaltar);
        }

        void esperarSaltar()
        {
            puedeSaltar = true;
        }

    }

     


}
