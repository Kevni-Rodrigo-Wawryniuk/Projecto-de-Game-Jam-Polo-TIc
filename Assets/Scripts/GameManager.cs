using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager gameManager;

    [Header("Pantallas")]
    [SerializeField] public Canvas[] pantallas;
    [SerializeField] public bool iniciarJuego, estasMuerto, ganaste;

    [Header("Slider Habilidad")]
    [SerializeField] Slider sliderHabilidad;

    [Header("Sonido")]
    [SerializeField] public AudioSource sonidoMenu;
    [SerializeField] public AudioSource sonidoPrincipal;

    [SerializeField] GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        sonidoMenu.Play();
        sonidoPrincipal.Stop();

        sliderHabilidad.value = 0;
        sliderHabilidad.maxValue = 3;
        sliderHabilidad.minValue = 0;
    }
    // Update is called once per frame
    void Update()
    { 
        ControlDePantallas();
    }
    
    public void ControlDePantallas()
    {
        // Pantalla De Inicio
        if(iniciarJuego == false && estasMuerto == false && ganaste == false)
        {
            pantallas[0].enabled = !pantallas[4].enabled;
            pantallas[2].enabled = false;
            pantallas[5].enabled = false;
        }

        // Pausar juego
        if (iniciarJuego == true && estasMuerto == false && ganaste == false)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");

            pantallas[0].enabled = false;
            pantallas[3].enabled = false;
            pantallas[2].enabled = false;
            pantallas[5].enabled = true;

            sliderHabilidad.value = jugador.GetComponent<MainPlayer>().hability;

        }

        // Pantalla Ganaste
        if (iniciarJuego == true && ganaste == true && estasMuerto == false)
        {
            pantallas[2].enabled = true;
            pantallas[0].enabled = false;
            pantallas[5].enabled = false;
            Time.timeScale = 0;

        }

        // Pantalla Fin de Juego

        if (iniciarJuego == true && estasMuerto == true && ganaste == false)
        {
            pantallas[3].enabled = true;
            pantallas[0].enabled = false;
            pantallas[5].enabled = false;
            Time.timeScale = 0;
        }
    }
}
