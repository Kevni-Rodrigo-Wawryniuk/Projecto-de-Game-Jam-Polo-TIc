using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField] public static PowerUP powerUP;

    [Header("componentes")]
    [SerializeField] Rigidbody2D rgbPU;
    [SerializeField] float fuerzaDeMovimietno;


    [Header("Rotar")]
    [SerializeField] float rotar;

    // Start is called before the first frame update
    void Start()
    {
        if (powerUP == null)
        {
            powerUP = this;
        }

        StartProgram();
    }

    // Update is called once per frame
    void Update()
    {
        MoverPower();
    }

    void StartProgram()
    {
        rgbPU = GetComponent<Rigidbody2D>();

        fuerzaDeMovimietno = Random.Range(6, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    void MoverPower()
    {

        transform.Rotate(new Vector3(0, 0, rotar * Time.deltaTime));

        rgbPU.velocity = new Vector2(-fuerzaDeMovimietno, rgbPU.velocity.y);

        if (transform.position.x <= -11)
        {
            Destroy(this.gameObject);
        }
    }
 
}
