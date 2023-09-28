using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool estaEnPausa = false;

    private void Update()
    {
        // Verificar si se presiona la tecla "Esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaEnPausa)
            {
                Reanudar(); // Si ya está en pausa, reanudar
            }
            else
            {
                Pausa(); // Si no está en pausa, pausar
            }
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        estaEnPausa = true; // Marcar que el juego está en pausa
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        estaEnPausa = false; // Marcar que el juego no está en pausa
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }
}