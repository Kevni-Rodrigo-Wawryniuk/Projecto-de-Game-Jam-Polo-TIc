using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] public static Obstaculo obstaculo;

    // Start is called before the first frame update
    void Start()
    {
        if (obstaculo == null)
        {
            obstaculo = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartProgram()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
