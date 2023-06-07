using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    [SerializeField] public static Enemy enemy;

    [Header("comportamiento")]
    [SerializeField] int fuerzaY;
    [SerializeField] public float tiempoM, finTiempoM;

    [Header("Lanzar cosas")]
    [SerializeField] GameObject[] obstaculos;
    [SerializeField] public int obstaculosint;
    [SerializeField] GameObject[] powerUP;
    [SerializeField] public int powerUpìnt;
    [SerializeField] public int valorInstancia;
    [SerializeField] public float timepoLC, fintiempoLC;

    [Header("Sonid Tirar Basura")]
    [SerializeField] AudioSource sonidoBasura;

    [SerializeField] GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null)
        {
            enemy = this;
        }
        gamemanager = GameObject.FindGameObjectWithTag("Manager");
        
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
        lanzarCosas();
        LosePLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    void Comportamiento()
    {

        tiempoM += 1 * Time.deltaTime;

        if (tiempoM >= finTiempoM)
        {
            fuerzaY = Random.Range(0, 17);

            if (fuerzaY >= 0 && fuerzaY <= 5)
            {
                transform.position = new Vector2(transform.position.x, -1.3f);
            }
            else if (fuerzaY >= 6 && fuerzaY <= 11)
            {
                transform.position = new Vector2(transform.position.x, -2.3f);
            }
            else if (fuerzaY >= 12 && fuerzaY <= 17)
            {
                transform.position = new Vector2(transform.position.x, -3.3f);
            }

            tiempoM = 0;
        }
    }
    public void lanzarCosas()
    {
        timepoLC += Time.deltaTime;

        if (timepoLC >= fintiempoLC)
        {
            timepoLC = 0;

            if (gamemanager.GetComponent<GameManager>().dificultad == 1)
            {

                fintiempoLC = Random.Range(1, 1.5f);
                finTiempoM = Random.Range(1, 1.5f);
                valorInstancia = Random.Range(2, 6);
                obstaculosint = Random.Range(0, 3);
                powerUpìnt = Random.Range(0, 2);
            }

            if (gamemanager.GetComponent<GameManager>().dificultad == 2)
            {

                fintiempoLC = Random.Range(0.5f, 1);
                finTiempoM = Random.Range(0.5f, 1);
                valorInstancia = Random.Range(0, 6);
                obstaculosint = Random.Range(0, 3);
                powerUpìnt = Random.Range(0, 2);
            }
            
            if (valorInstancia >= 0 && valorInstancia <= 3)
            {
                if (obstaculosint == 0)
                {
                    Instantiate(obstaculos[0], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
                else if (obstaculosint == 1)
                {
                    Instantiate(obstaculos[1], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
                else if (obstaculosint == 2)
                {
                    Instantiate(obstaculos[2], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
                else if (obstaculosint == 3)
                {
                    Instantiate(obstaculos[3], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
            }

            if (valorInstancia >= 3 && valorInstancia <= 6)
            {
                if (powerUpìnt == 0)
                {
                    Instantiate(powerUP[0], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
                else if (powerUpìnt == 1)
                {
                    Instantiate(powerUP[1], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
                else if (powerUpìnt == 2)
                {
                    Instantiate(powerUP[2], transform.position, Quaternion.identity);
                    sonidoBasura.Play();
                }
            }
        }
    }
    void LosePLayer()
    {
       if (gamemanager.GetComponent<GameManager>().estasMuerto == true)
        {
            Destroy(this.gameObject);
        }
    }
}
