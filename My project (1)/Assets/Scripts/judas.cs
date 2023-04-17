using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judas : MonoBehaviour
{
    //Fuerza con la que se mueve el jugador
    private float movimientoFuerza = 10f;

    private Rigidbody2D miCuerpoRigido;

    // Start is called before the first frame update
    void Start()
    {
        miCuerpoRigido = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal"); //esta variable puede ser -1, 0 o 1
        Vector2 posicionJug = transform.position;

        posicionJug = posicionJug + new Vector2(movementX, 0f) * movimientoFuerza * Time.deltaTime;

        transform.position = posicionJug;
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

}
