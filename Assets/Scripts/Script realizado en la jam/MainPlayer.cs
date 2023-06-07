using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] public static MainPlayer mainPlayer;

    [Header("Movimiento")]
    [SerializeField] public bool mover;
    [SerializeField] float vectorY;

    [Header("fuerza de retraso")]
    [SerializeField] float fuerzaX;
    [SerializeField] bool obstaculo, powerUp;
    [SerializeField] float tiempoEP, finTiempoEP;

    [Header("habilidad")]
    [SerializeField] float tiempoH;
    [SerializeField] public float hability;
    [SerializeField] bool habilidadActiva;

    [Header("Sonido")]
    [SerializeField] AudioSource sonidoHabilidad;
    [SerializeField] AudioSource[] sonidoFrutas;
    [SerializeField] AudioSource sonidoChoque;

    [Header("Particulas")]
    [SerializeField] ParticleSystem particulas;

    [Header("GameManager")]
    [SerializeField] GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        if (mainPlayer == null)
        {
            mainPlayer = this;
        }
        StartProgram();
    }

    // Update is called once per frame
    void Update()
    {
        Moverce();
        CuandoColliciona();
        Perder();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rigth"))
        {
            if(hability == 0)
            {
                sonidoFrutas[0].Play();
            }
            else if (hability == 1)
            {
                sonidoFrutas[1].Play();
            }
            else if (hability >= 2)
            {
                sonidoFrutas[2].Play();
            }
            powerUp = true;
            if (hability < 3)
            {
                hability++;
            }
        }

        if (collision.gameObject.CompareTag("Left"))
        {
            obstaculo = true;
            sonidoChoque.Play();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            gamemanager.GetComponent<GameManager>().iniciarJuego = true;
            gamemanager.GetComponent<GameManager>().estasMuerto = false;
            gamemanager.GetComponent<GameManager>().ganaste = true;
            mover = false;
            Destroy(this.gameObject);
            gamemanager.GetComponent<GameManager>().sonidoMenu.Play();
            gamemanager.GetComponent<GameManager>().sonidoPrincipal.Stop();
        }
    }

    void StartProgram()
    {
        hability = 0;
        mover = true;

        gamemanager = GameObject.FindGameObjectWithTag("Manager");
    }

    void Moverce()
    {
        if (mover == true)
        {
            // Mover al personaje por los lugares disponibles
            if( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }        
            // Limitar los movimietnos
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, -12, 9), Mathf.Clamp(transform.position.y, -3.3f, -1.3f));


            if (hability >= 3)
            {
                habilidadActiva = true;
            }
           
            // Usar la habilidad 
            if (Input.GetKeyDown(KeyCode.Space) && hability == 3 && habilidadActiva == true)
            {
                StartCoroutine(Habilidad());
                hability = 0;
                sonidoHabilidad.Play();
                particulas.Play();
            }
        }
    }
    void CuandoColliciona()
    {
        if (tiempoEP >= finTiempoEP)
        {
            tiempoEP = 0;
            powerUp = false;
            obstaculo = false;
        }

        // Colisiona con una fruta
        if (powerUp == true)
        {
            tiempoEP += 1 * Time.deltaTime;

            if (tiempoEP < finTiempoEP)
            {
                transform.position = new Vector2(transform.position.x + fuerzaX * Time.deltaTime, transform.position.y);
            }

            if (obstaculo == true)
            {
                powerUp = false;
            }
        }
        // Colisiona con una basura
        if (obstaculo == true)
        {
            tiempoEP += 1 * Time.deltaTime;

            if (tiempoEP < finTiempoEP)
            {
                transform.position = new Vector2(transform.position.x - fuerzaX * Time.deltaTime, transform.position.y);
            }

            if (powerUp == true)
            {
                obstaculo = false;
            }
        }
    }
    void Perder()
    {
        if (transform.position.x < -9)
        {
            gamemanager.GetComponent<GameManager>().iniciarJuego = true;
            gamemanager.GetComponent<GameManager>().estasMuerto = true;
            gamemanager.GetComponent<GameManager>().ganaste = false;
            mover = false;
            gamemanager.GetComponent<GameManager>().sonidoMenu.Play();
            gamemanager.GetComponent<GameManager>().sonidoPrincipal.Stop();
            Destroy(this.gameObject);
        }
    }
    IEnumerator Habilidad()
    {
        Physics2D.IgnoreLayerCollision(0, 6);
        mover = false;
        obstaculo = false;
        powerUp = true;
        habilidadActiva = true;

        yield return new WaitForSeconds(tiempoH);

        mover = true;
        powerUp = false;
        obstaculo = false;
        habilidadActiva = false;
    }
}
