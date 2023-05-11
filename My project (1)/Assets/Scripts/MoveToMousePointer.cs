using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMousePointer : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool toco;

    private void Start()
    {
        toco = false;
    }

    private void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = transform.position.z;
        
        if (Input.GetMouseButtonDown(0))
        {
            toco = true;
        }

        if (Vector2.Distance(transform.position, worldPoint) > 0.1f && toco)
        {
            transform.position = Vector2.Lerp(transform.position, worldPoint, velocidad * Time.deltaTime);
        }
        else
        {
            toco = false;
        }
    }
}