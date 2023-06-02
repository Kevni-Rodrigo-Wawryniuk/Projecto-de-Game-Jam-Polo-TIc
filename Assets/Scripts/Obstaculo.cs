using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] public static Obstaculo obstaculo;

    [Header("componentes")]
    [SerializeField] Rigidbody2D RgbObs;
    [SerializeField] float fuerzaDeMovimiento;

    [Header("Rotacion")]
    [SerializeField] float Rotar;

    // Start is called before the first frame update
    void Start()
    {
        if (obstaculo == null)
        {
            obstaculo = this;
        }
        StartProgram();
    }

    // Update is called once per frame
    void Update()
    {
        MoverOpstaculo();
    }

    void StartProgram()
    {
        RgbObs = GetComponent<Rigidbody2D>();

        if (ControlBotton.controlBotton.dificultad == 0)
        {
            fuerzaDeMovimiento = Random.Range(12, 16);
        }
        if (ControlBotton.controlBotton.dificultad == 1)
        {
            fuerzaDeMovimiento = Random.Range(16, 20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void MoverOpstaculo()
    {

        transform.Rotate(new Vector3(0, 0, Rotar * Time.deltaTime));

        RgbObs.velocity = new Vector2(-fuerzaDeMovimiento, RgbObs.velocity.y);

        if(transform.position.x <= -11)
        {
            Destroy(this.gameObject);
        }
    }
    
}
