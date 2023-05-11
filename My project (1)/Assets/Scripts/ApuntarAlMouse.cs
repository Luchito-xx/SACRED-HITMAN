using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarAlMouse : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //Obtención de la ubicación del mouse
        Vector3 punto = cam.ScreenToWorldPoint(Input.mousePosition);

        // Obtención del ángulo
        Vector3 diferencia = (punto - transform.position).normalized;
        float angulo = Mathf.Atan2(diferencia.y, diferencia.x) * Mathf.Rad2Deg;

        // Rotación del ángulo
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angulo - 90);
        transform.rotation = rotation;
    }
}
