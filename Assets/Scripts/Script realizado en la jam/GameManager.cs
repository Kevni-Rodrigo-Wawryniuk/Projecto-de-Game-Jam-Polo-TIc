using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager gameManager;

    [Header("Pantallas")]
    [SerializeField] public Canvas[] pantallas;
    [SerializeField] bool pantalla0, pantalla1, pantalla2, pantalla3, pantalla4, pantalla5;

    [Header("Estados Del Juego")]
    [SerializeField] public bool iniciarJuego, estasMuerto, ganaste;

    [Header("Slider Habilidad")]
    [SerializeField] Slider sliderHabilidad;

    [Header("Sonido")]
    [SerializeField] public AudioSource sonidoMenu;
    [SerializeField] public AudioSource sonidoPrincipal;
    [SerializeField] public AudioSource sonidoBoton;

    [Header("Jugador Y Enemigo")]
    [SerializeField] GameObject jugador;
    [SerializeField] GameObject Enemy;

    [Header("Objetos en escena")]
    [SerializeField] GameObject PlayerPlay;

    [Header("Mover El Esenario")]
    [SerializeField] MeshRenderer[] meshRenderersFondo;
    [SerializeField] float velocidadFondo0, velocidadFondo1, velocidadFondo2, velocidadFondo3, velocidadFondo4;

    [Header("Dficult")]
    [SerializeField] public int dificultad;
    [SerializeField] Image[] imagesDificultad;
    [SerializeField] bool imagenesActivas;

    [Header("Keyboard Control")]
    [SerializeField] GameObject[] keyboardControl, keyboardControl1, keyboardControl2, keyboardControl3;
    [SerializeField] int keyboadValue, keyboardValue1, keyboardValue2, keyboardValue3;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        StartProgram();
    }
    // Update is called once per frame
    void Update()
    {
        ControlPorTeclado();
        ControlDePantallas();
        MoverFondo();
    }
    void StartProgram()
    {
        sliderHabilidad.maxValue = 3;
        sliderHabilidad.minValue = 0;

        dificultad = 1;

        sonidoMenu.Play();
        sonidoPrincipal.Stop();

        pantalla0 = true; 
        pantalla1 = false;
        pantalla2 = false;
        pantalla3 = false;
        pantalla4 = false;
        pantalla5 = false;
    }

    public void BotonPantallaDificultad()
    {
        pantalla0 = !pantalla0;
        pantalla1 = !pantalla1;
        sonidoBoton.Play();
    }
    public void BotonSalirDelJuego()
    {
        if (pantalla0 == true && iniciarJuego == false && estasMuerto == false && ganaste == false || 
            iniciarJuego == true && estasMuerto == true && ganaste == false && pantalla5 == true ||
            iniciarJuego == true && estasMuerto == false && ganaste == true && pantalla4 == true)
        {
            sonidoBoton.Play();
            Application.Quit();
        }
    }
    public void BotonIniciarJuego()
    {
        if (iniciarJuego == false)
        {
            sonidoBoton.Play();

            iniciarJuego = true;
            estasMuerto = false;
            ganaste = false;

            pantalla2 = true;

            sonidoMenu.Stop();
            sonidoPrincipal.Play();

            Instantiate(jugador, new Vector2(-5, -2.3f), Quaternion.identity);
            Instantiate(Enemy, new Vector2(5, -2.3f), Quaternion.identity);
        }
    }
    public void BotonRecargaEscena()
    {
        sonidoBoton.Play();

        SceneManager.LoadScene(0);
    }
    public void BotonSeleccionarDificultad()
    {
        sonidoBoton.Play();

        if (dificultad > 1)
        {
            dificultad = 0;
        }

        imagenesActivas = !imagenesActivas;

        dificultad++;
    }

    void ControlDePantallas()
    {
        // imagenes de dificultad
        imagesDificultad[0].enabled = imagenesActivas;
        imagesDificultad[1].enabled = !imagenesActivas;

        // Pantallas
        pantallas[0].enabled = pantalla0;
        pantallas[1].enabled = pantalla1;
        pantallas[2].enabled = pantalla2;
        pantallas[3].enabled = pantalla3;
        pantallas[4].enabled = pantalla4;
        pantallas[5].enabled = pantalla5;
       
        // Estado de las pantalla al iniciar
        if (iniciarJuego == false && estasMuerto == false && ganaste == false)
        {
            pantalla2 = false;
            pantalla3 = false;
            pantalla4 = false;
            pantalla5 = false;
        }
        // Estado de cuando se esta jugando
        else if (iniciarJuego == true && estasMuerto == false && ganaste == false)
        {
            pantalla0 = false;
            pantalla1 = false;
            pantalla4 = false;
            pantalla5 = false;
            PauseJuego();

            PlayerPlay = GameObject.FindGameObjectWithTag("Player");

            sliderHabilidad.value = PlayerPlay.GetComponent<MainPlayer>().hability;
        }
        else if (iniciarJuego == true && estasMuerto == true && ganaste == false)
        {
            pantalla0 = false;
            pantalla1 = false;
            pantalla2 = false;
            pantalla3 = false;
            pantalla4 = false;
            pantalla5 = true;
        }
        else if (iniciarJuego == true && estasMuerto == false && ganaste == true)
        {
            pantalla0 = false;
            pantalla1 = false;
            pantalla2 = false;
            pantalla3 = false;
            pantalla4 = true;
            pantalla5 = false;
        }
    }
    void PauseJuego()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pantalla2 = !pantalla2;
            pantalla3 = !pantalla3;

            PlayerPlay.GetComponent<MainPlayer>().mover = !PlayerPlay.GetComponent<MainPlayer>().mover;
        }

        if (pantalla3 == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void MoverFondo()
    {
        meshRenderersFondo[0].material.mainTextureOffset = meshRenderersFondo[0].material.mainTextureOffset += new Vector2(velocidadFondo0 * Time.deltaTime, 0);
        meshRenderersFondo[1].material.mainTextureOffset = meshRenderersFondo[1].material.mainTextureOffset += new Vector2(velocidadFondo1 * Time.deltaTime, 0);
        meshRenderersFondo[2].material.mainTextureOffset = meshRenderersFondo[2].material.mainTextureOffset += new Vector2(velocidadFondo2 * Time.deltaTime, 0);
        meshRenderersFondo[3].material.mainTextureOffset = meshRenderersFondo[3].material.mainTextureOffset += new Vector2(velocidadFondo3 * Time.deltaTime, 0);
        meshRenderersFondo[4].material.mainTextureOffset = meshRenderersFondo[4].material.mainTextureOffset += new Vector2(velocidadFondo4 * Time.deltaTime, 0);
    } // Con esto movemos el fondo y la carretera
    void ControlPorTeclado()
    { 
                // Panalla Inicial
             if (iniciarJuego == false && estasMuerto == false && ganaste == false && pantalla0 == true)
        {
            if (keyboadValue < 0)
            {
                keyboadValue = 2;
            }
            else if (keyboadValue > 2)
            {
                keyboadValue = 0;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                keyboadValue++;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyboadValue--;
            }

            switch (keyboadValue)
            {
                default:
                    keyboardControl[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    keyboardControl[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl[2].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonIniciarJuego();
                    }
                    break;
                case 1:
                    keyboardControl[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    keyboardControl[2].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonPantallaDificultad();
                    }
                    break;
                case 2:
                    keyboardControl[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl[2].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonSalirDelJuego();
                    }
                    break;
            }
        }
             // Pantalla Seleccionar dificultad
        else if (iniciarJuego == false && estasMuerto == false && ganaste == false && pantalla1 == true)
        {
            if (keyboardValue3 < 0)
            {
                keyboardValue3 = 1;
            }
            if (keyboardValue3 > 1)
            {
                keyboardValue3 = 0;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                keyboardValue3++;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyboardValue3--;
            }

            switch (keyboardValue3)
            {
                default:
                    keyboardControl3[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    keyboardControl3[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonSeleccionarDificultad();
                    }
                    break;
                case 1:
                    keyboardControl3[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl3[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonPantallaDificultad();
                    }
                    break;
            }
        }
             // Pantalla EstasMuerto
        else if (iniciarJuego == true && estasMuerto == true && ganaste == false && pantalla5 == true)
        {
            if (keyboardValue1 < 0)
            {
                keyboardValue1 = 1;
            }
            if (keyboardValue1 > 1)
            {
                keyboardValue1 = 0;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                keyboardValue1++;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyboardValue1--;
            }

            switch (keyboardValue1)
            {
                default:
                    keyboardControl1[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    keyboardControl1[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonRecargaEscena();
                    }
                    break;
                case 1:
                    keyboardControl1[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl1[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonSalirDelJuego();
                    }
                    break;
            }
        }
             // Pantalla Ganaste
        else if (iniciarJuego == true && estasMuerto == false && ganaste == true && pantalla4 == true)
        {
            if (keyboardValue2 < 0)
            {
                keyboardValue2 = 1;
            }
            if (keyboardValue2 > 1)
            {
                keyboardValue2 = 0;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                keyboardValue2++;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyboardValue2--;
            }

            switch (keyboardValue2)
            {
                default:
                    keyboardControl2[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    keyboardControl2[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonRecargaEscena();
                    }
                    break;
                case 1:
                    keyboardControl2[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    keyboardControl2[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        BotonSalirDelJuego();
                    }
                    break;
            }
        }
    }
}
