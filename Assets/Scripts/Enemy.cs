using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    [SerializeField] public static Enemy enemy;

    [Header("comportamiento")]
    [SerializeField] int fuerzaY;
    [SerializeField] float tiempoM, finTiempoM;

    [Header("Lanzar cosas")]
    [SerializeField] GameObject[] obstaculos;
    [SerializeField] int obstaculosint;
    [SerializeField] GameObject[] powerUP;
    [SerializeField] int powerUpìnt;
    [SerializeField] int valorinstancia;
    [SerializeField] float timepoLC, fintiempoLC;

    [Header("Sonid Tirar Basura")]
    [SerializeField] AudioSource sonidoBasura;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null)
        {
            enemy = this;
        }

        StartProgram();
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
        lanzarCosas();
    }

    void StartProgram()
    {
        if (ControlBotton.controlBotton.dificultad == 0)
        {
            finTiempoM = Random.Range(1, 2);
            fintiempoLC = Random.Range(2, 4);
        }
        if (ControlBotton.controlBotton.dificultad == 1)
        {
            finTiempoM = Random.Range(0.1f, 0.5f);
            fintiempoLC = Random.Range(1, 1.5f);
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
        if (GameManager.gameManager.iniciarJuego == true && GameManager.gameManager.estasMuerto == false && GameManager.gameManager.ganaste == false)
        {

            timepoLC += Time.deltaTime;

            if (timepoLC >= fintiempoLC)
            {
                valorinstancia = Random.Range(0, 6);
                obstaculosint = Random.Range(0, 3);
                powerUpìnt = Random.Range(0, 2);
                timepoLC = 0;

                if (valorinstancia >= 0 && valorinstancia <= 3)
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
                else if (valorinstancia >= 3 && valorinstancia <= 6)
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
    }
}
