using UnityEngine;

public class MoverHaciaElMouse : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float velocidad= 100f;

    private Vector3 puntoMundo;
    void Start()
    {
        cam = Camera.main;
        if (!cam)
            Debug.LogWarning("No se encontró la cámara principal");
    }

    void Update()
    {
       MoviendoHaciaElMouse();
    }

public void MoviendoHaciaElMouse()
{
//Obtención del punto del mouse en pantalla
Vector3 punto = cam.ScreenToWorldPoint(Input.mousePosition);

//----------------------------- Rotación del objeto ---------------------------------
//El sprite está mirando hacia la izquierda y va a rotar hacia el punto conseguido por el mouse
//Para que el objeto siga visible en el plano se coloca el eje z a la misma distancia en donde renderiza la cámara

punto.z = cam.nearClipPlane;
puntoMundo = cam.ScreenToViewportPoint(punto);


//Rotación derecha o izquierda según sea la posición en x del mouse vs el objeto
Quaternion rotacion = new Quaternion();

if (punto.x > transform.position.x){
   rotacion.eulerAngles = new Vector3(0, 180, 0);
}else{ 
   rotacion.eulerAngles = new Vector3(0, 0, 0);
}
   transform.rotation = rotacion;

//----------------------------- Movimiento ---------------------------------
//a mayor distancia mayor velocidad y viceversa

float distancia = Vector3.Distance(transform.position, punto);
float velocidadSegunDistancia = distancia / velocidad; 

transform.position = (Vector3.Lerp(transform.position, punto, velocidadSegunDistancia));
}
}//Fin Clase

