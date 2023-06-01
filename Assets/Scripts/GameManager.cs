using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager gameManager;

    [Header("Lanzar cosas")]
    [SerializeField] GameObject obstaculos;
    [SerializeField] GameObject powerUP;
    [SerializeField] float timepoLC, fintiempoLC;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lanzarCosas()
    {
        timepoLC += Time.deltaTime;

        if (timepoLC >= fintiempoLC)
        {
            timepoLC = 0;

        }
    }
}
