using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] public static Background background;

    [Header("Mover fondo")]
    [SerializeField] MeshRenderer fondo;
    [SerializeField] float offSet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moverFondo();
    }

    void moverFondo()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset += new Vector2(offSet * Time.deltaTime, 0);
    }
}
