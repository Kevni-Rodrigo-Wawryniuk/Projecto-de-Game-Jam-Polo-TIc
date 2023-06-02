using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ControlBotton : MonoBehaviour
{
    [SerializeField] public static ControlBotton controlBotton;

    [Header("Botones")]
    [SerializeField] GameObject[] botonesIniciales;
    [SerializeField] GameObject[] botonesDificultad;
    [SerializeField] GameObject[] botonesGanaste;
    [SerializeField] GameObject[] botonesPerdiste;

    [SerializeField] Image[] imagenBoton;

    [SerializeField] int valorBotonesIniciales, valorBotonesPerdiste, valorBotonesGanaste;

    [SerializeField] bool inicio, muerto, ganar;
    [SerializeField] public int dificultad;

    [Header("Colocar Jugador y Enemigo")]
    [SerializeField] GameObject[] jugadorYEnemigo;

    [Header("Sonido Botones")]
    [SerializeField] AudioSource sonidoBotones;

    // Start is called before the first frame update
    void Start()
    {
        if(controlBotton == null)
        {
            controlBotton = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlPantallaInicial();
        ControlPantallaGanaste();
        ControlPantallaPerdiste();
    }

    void ControlPantallaInicial()
    {
        inicio = GameManager.gameManager.iniciarJuego;
        muerto = GameManager.gameManager.estasMuerto;
        ganar = GameManager.gameManager.ganaste;

        if (inicio == false && muerto == false && ganar == false)
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                valorBotonesIniciales--;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                valorBotonesIniciales++;
            }

            if (valorBotonesIniciales < 0)
            {
                valorBotonesIniciales = 2;
            }
            if (valorBotonesIniciales > 2)
            {
                valorBotonesIniciales = 0;
            }

           switch(valorBotonesIniciales)
            {
                case 1:
                    botonesIniciales[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesIniciales[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    botonesIniciales[2].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        SeleccionarDificultad();
                    }
                    break;
                case 2:
                    botonesIniciales[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesIniciales[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesIniciales[2].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        Salir();
                    }

                    break;
                default:
                    botonesIniciales[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f,1.1f,1);
                    botonesIniciales[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesIniciales[2].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonInicarJuego();
                    }
                    break;
            }
        }
    }
    void ControlPantallaGanaste()
    {
        if (inicio == true && muerto == false && ganar == true)
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                valorBotonesGanaste--;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                valorBotonesGanaste++;
            }

            if (valorBotonesGanaste < 0)
            {
                valorBotonesGanaste = 2;
            }
            if (valorBotonesGanaste > 2)
            {
                valorBotonesGanaste = 0;
            }

            switch (valorBotonesGanaste)
            {
                case 1:
                    botonesGanaste[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesGanaste[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

                    if(Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        Salir();
                    }

                    break;

                default:
                    botonesGanaste[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    botonesGanaste[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
          
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        VolverAJugar();
                    }
                    break;
            }
        }
    }
    void ControlPantallaPerdiste()
    {

        if (inicio == true && muerto == true && ganar == false)
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                valorBotonesPerdiste--;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                valorBotonesPerdiste++;
            }

            if (valorBotonesPerdiste < 0)
            {
                valorBotonesPerdiste = 2;
            }
            if (valorBotonesPerdiste > 2)
            {
                valorBotonesPerdiste = 0;
            }

            switch (valorBotonesPerdiste)
            {
                case 1:
                    botonesPerdiste[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    botonesPerdiste[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        Salir();
                    }

                    break;

                default:
                    botonesPerdiste[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    botonesPerdiste[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        VolverAJugar();
                    }
                    break;
            }
        }
    }

    public void BotonInicarJuego()
    {
        if (dificultad == 0)
        {
            GameManager.gameManager.iniciarJuego = !GameManager.gameManager.iniciarJuego;
            // Coloca el jugador
            Instantiate(jugadorYEnemigo[0], transform.position = new Vector2(-6, -3), Quaternion.identity);
            //coloca al enemigo
            Instantiate(jugadorYEnemigo[1], transform.position = new Vector2(7, -3), Quaternion.identity);
            sonidoBotones.Play();
            GameManager.gameManager.sonidoMenu.Stop();
            GameManager.gameManager.sonidoPrincipal.Play();
        }

        if (dificultad == 1)
        {
            GameManager.gameManager.iniciarJuego = !GameManager.gameManager.iniciarJuego;
            // Coloca el jugador
            Instantiate(jugadorYEnemigo[0], transform.position = new Vector2(-6, -2.3f), Quaternion.identity);
            //coloca al enemigo
            Instantiate(jugadorYEnemigo[1], transform.position = new Vector2(7, -2.3f), Quaternion.identity);
            sonidoBotones.Play();
            GameManager.gameManager.sonidoMenu.Stop();
            GameManager.gameManager.sonidoPrincipal.Play();
        }

    }

    public void SeleccionarDificultad()
    {
        GameManager.gameManager.pantallas[4].enabled = !GameManager.gameManager.pantallas[4].enabled;
        sonidoBotones.Play();
    }
    public void Facil()
    {
        
        if (dificultad == 0)
        {
            dificultad = 1;
            sonidoBotones.Play();
            imagenBoton[0].enabled = true;
            imagenBoton[1].enabled = false;
        }
        else if (dificultad == 1)
        {
            imagenBoton[1].enabled = true;
            imagenBoton[0].enabled = false;

            dificultad = 0;
            sonidoBotones.Play();
        }
    }
    
    public void Salir()
    {
        Application.Quit();
        sonidoBotones.Play();
        Debug.Log("saliste del juego");
    }
    public void VolverAJugar()
    {
        SceneManager.LoadScene(0);
        sonidoBotones.Play();
        Debug.Log("Reiciniaste el juego");
    }
}
