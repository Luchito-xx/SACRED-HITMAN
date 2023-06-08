﻿using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] private    Vector2 velocidadRebote;
    [SerializeField] private float factorGravedad = 2.8f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private ActivadorSalto      m_activadorSalto;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private Collider2D          banditCLD;

    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        // m_activadorSalto = transform.Find("ActivadorSalto").GetComponent<ActivadorSalto>();
    }
	
	void Update () {
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        else if(Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
        }

        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        else
            m_animator.SetInteger("AnimState", 0);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Piso>())
        {
            PuedeSaltar();
            m_body2d.gravityScale = 1;
        }
    }



    //Parametros movimiento celular
    bool enizquierda = false;
    bool enderecha = false;
    bool puedeSaltar = false;
    bool pegar = false;
    bool puedePegar = true;
    private bool saltar = false;

    public float fuerzaVelocidad;
    public float fuerzaSalto;

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
        if (puedeSaltar) 
        {
            saltar = true;
        }
    }

    public void clickPegar()
    {
        pegar = true;
        m_animator.SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
        if (enizquierda)
        {
            m_body2d.AddForce(new Vector2(-fuerzaVelocidad, 0)* Time.deltaTime);
        }

        if (enderecha)
        {
            m_body2d.AddForce(new Vector2(fuerzaVelocidad, 0)* Time.deltaTime);
        }

        if (saltar)
        {
            m_body2d.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            m_animator.SetTrigger("Jump");
            puedeSaltar = false;
            saltar = false;
            m_body2d.gravityScale = factorGravedad;
        }

        if (pegar && puedePegar)
        {
            pegar = false;
        }

    }

    // //conseguir daño
    // private void getDaño()
    // {
    //     if (angelCLD.position == banditCLD.position)
    //     {
    //         animDaño();
    //     }
    // }

    // private void animDaño()
    // {
    //     m_animator.SetTrigger("Hurt");
    // }

        public void PuedeSaltar()
        {
            puedeSaltar = true;
        }
}
