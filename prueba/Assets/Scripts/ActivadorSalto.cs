using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorSalto : MonoBehaviour
{
    private Bandit noPuedeSaltar;

    public void Start()
    {
        noPuedeSaltar = transform.Find("Bandit").GetComponent<Bandit>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Techo")
        {
            noPuedeSaltar.NoPuedeSaltar();
        }

        if (collision.gameObject.tag != "Techo")
        {
            noPuedeSaltar.SiPuedeSaltar();
        }
    }

}
