using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainPlayer : MonoBehaviour
{
    [SerializeField] public static MainPlayer mainPlayer;

    [Header("componentes")]
    [SerializeField] Rigidbody2D rgbdMp;

    [Header("Movimiento")]
    [SerializeField] bool mover;
    [SerializeField] float vectorX, vectorY;

    [Header("fuerza de retraso")]
    [SerializeField] float empuje;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rigth"))
        {
            rgbdMp.velocity = new Vector2(empuje, rgbdMp.velocity.y);
        }

        if (collision.gameObject.CompareTag("Left"))
        {
            rgbdMp.velocity = new Vector2(-empuje, rgbdMp.velocity.y);
        }
    }

    void StartProgram()
    {
        rgbdMp = GetComponent<Rigidbody2D>();
    }

    void Moverce()
    {
        if (mover == true)
        {
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

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -9 ,9), Mathf.Clamp(transform.position.y, -2, 2));
        }
    }
 
    
}
