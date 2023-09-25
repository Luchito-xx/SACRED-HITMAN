using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] private    Vector2 velocidadRebote;
    [SerializeField] private float factorGravedad = 2.8f;
    [SerializeField] private    Transform lava;
    [SerializeField] private    GameObject panelDerrota;
    [SerializeField] private    Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private    GameObject bandido;

    private SpriteRenderer      mySpriteRenderer;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    public GameObject[]         vida;
    public GameObject[] espadas; 
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private Collider2D          banditCLD;
    public AudioSource          lava_sound;
    private int                 vidas = 3;
    private bool                muerte = false;
    private bool activopoder = false;

    public void ActivarPoder(){
        activopoder=true;
    }

    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    
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

        if(CalculaDistancia() < 10f )
        {
            if (!lava_sound.isPlaying)
            {
                lava_sound.Play();
            }
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Piso>())
        {
            PuedeSaltar();
            m_body2d.gravityScale = 1;
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            PerderVida();
        }

    }



    //Parametros movimiento celular
    bool enizquierda = false;
    bool enderecha = false;
    bool puedeSaltar = false;
    bool pegar = false;
    bool puedePegar = true;
    bool puedepoder = true;
    private bool saltar = false;

    public float fuerzaVelocidad;
    public float fuerzaSalto;

    public void clickIzquierda()
    {
        enizquierda = true;
        m_animator.SetTrigger("Run");
        
    }

    public void releaseIzquierda()
    {
        enizquierda = false;
        m_animator.SetTrigger("Idle");
    }

    public void clickDerecha()
    {
        enderecha = true;
        m_animator.SetTrigger("Run");
    }

    public void releaseDerecha()
    {
        enderecha = false;
        m_animator.SetTrigger("Idle");
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
            mySpriteRenderer.flipX = false;
        }

        if (enderecha)
        {
            m_body2d.AddForce(new Vector2(fuerzaVelocidad, 0)* Time.deltaTime);
            mySpriteRenderer.flipX = true;
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
            Golpe();
        }

    }

    public void PuedeSaltar()
    {
        puedeSaltar = true;
    }


    public float CalculaDistancia()
        {
            float distancia = Vector3.Distance(transform.position,lava.position);
            RegularVolumen(distancia);
            return distancia;
        }

    public void RegularVolumen(float distancia)
    {
        if (distancia <= 0 )
        {
            lava_sound.volume = 1;
        } else {
            lava_sound.volume = 1f/distancia;
        }
    }

    public void DesactivarVida(int indice)
    {
        vida[indice].SetActive(false);
    }

    public void ActivarVidas(int indice)
    {
        vida[indice].SetActive(true);
    }

    public void PerderVida()
    {
        vidas -= 1;
        DesactivarVida(vidas);
        Debug.Log(vidas);
        if (vidas <= 0)
        {
            muerte = true;
        }
        if(muerte)
        {
            panelDerrota.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
    


    public void ActivadorPoder()
    {
        
        if (activopoder)
        {
            PoderActivo();
        }
    }
    
    
    public void PoderActivo()
    {
        foreach(GameObject elemento in espadas)
        {
            elemento.transform.position = new Vector2(elemento.transform.position.x, -0.9f);
        }
    }

}
