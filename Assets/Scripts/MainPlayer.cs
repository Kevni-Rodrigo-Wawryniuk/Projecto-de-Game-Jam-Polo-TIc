using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] public static MainPlayer mainPlayer;

    [Header("componentes")]
    [SerializeField] Rigidbody2D rgbdMp;
    [SerializeField] BoxCollider2D cajaDeColicion;

    [Header("Movimiento")]
    [SerializeField] public bool mover;
    [SerializeField] float vectorY;

    [Header("fuerza de retraso")]
    [SerializeField] float fuerzaX;
    [SerializeField] bool obstaculo, powerUp;
    [SerializeField] float tiempoEP, finTiempoEP;
    [SerializeField] public bool pausa;

    [Header("habilidad")]
    [SerializeField] float tiempoH;
    [SerializeField] public float hability;

    [Header("Sonido")]
    [SerializeField] AudioSource sonidoHabilidad;
    [SerializeField] AudioSource[] sonidoFrutas;
    [SerializeField] AudioSource sonidoChoque;

    [Header("Particulas")]
    [SerializeField] ParticleSystem particulas;

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
        PausarJuego();
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
            hability += 1;
        }

        if (collision.gameObject.CompareTag("Left"))
        {
            obstaculo = true;
            sonidoChoque.Play();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.gameManager.ganaste = true;
            mover = false;
            Destroy(this.gameObject);
            GameManager.gameManager.sonidoMenu.Play();
            GameManager.gameManager.sonidoPrincipal.Stop();
        }
    }

    void StartProgram()
    {
        rgbdMp = GetComponent<Rigidbody2D>();
        cajaDeColicion = GetComponent<BoxCollider2D>();
        mover = true;
        
    }

    void Moverce()
    {
 

        if (mover == true)
        {

            /*
            if (Input.GetKey(KeyCode.W))
            {
                rgbdMp.velocity = new Vector2(rgbdMp.velocity.x, vectorY);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rgbdMp.velocity = new Vector2(rgbdMp.velocity.x, -vectorY);
            }
            else
            {
                rgbdMp.velocity = new Vector2(0, 0);
            }
            */

            if( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }
            
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, -12, 9), Mathf.Clamp(transform.position.y, -3.3f, -1.3f));
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && hability >= 3)
        {
            StartCoroutine(Habilidad());
            hability = 0;
            sonidoHabilidad.Play();
            particulas.Play();
        }
    }
 
    public void PausarJuego()
    {
        if (GameManager.gameManager.iniciarJuego == true && GameManager.gameManager.estasMuerto == false)
        {
            GameManager.gameManager.pantallas[1].enabled = pausa;
            
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                pausa = !pausa;
                mover = !mover;
            }

            if (pausa == true)
            {
                Time.timeScale = 0;
            }
            else if (pausa == false)
            {
                Time.timeScale = 1;
            }
        }
    }
    void CuandoColliciona()
    {
        // Colisiona con una fruta
        if (powerUp == true && obstaculo == false && tiempoEP < finTiempoEP)
        {
            tiempoEP += 1 * Time.deltaTime;

            rgbdMp.velocity = new Vector2(fuerzaX, rgbdMp.velocity.y);
        }

           // Colisiona con una basura
        if (obstaculo == true && powerUp == false && tiempoEP < finTiempoEP)
        {
            tiempoEP += 1 * Time.deltaTime;

            rgbdMp.velocity = new Vector2(-fuerzaX, rgbdMp.velocity.y);
        }

        if (tiempoEP >= finTiempoEP)
        {
            powerUp = false;
            obstaculo = false;
            tiempoEP = 0;

            rgbdMp.velocity = new Vector2(0, rgbdMp.velocity.y);
        }
        if (obstaculo == true && powerUp == true)
        {
            obstaculo = false;
            powerUp = false;
            tiempoEP = 0;

            rgbdMp.velocity = new Vector2(0, rgbdMp.velocity.y);
        }
    }
    void Perder()
    {
        if (transform.position.x < -9)
        {
            GameManager.gameManager.estasMuerto = true;
            mover = false;
            GameManager.gameManager.sonidoMenu.Play();
            GameManager.gameManager.sonidoPrincipal.Stop();
            Destroy(this.gameObject);
        }
    }
    IEnumerator Habilidad()
    {
        Physics2D.IgnoreLayerCollision(0, 6);
        mover = false;
        obstaculo = false;
        powerUp = true;

        yield return new WaitForSeconds(tiempoH);

        mover = true;
        powerUp = false;
        obstaculo = false;
    }
}
