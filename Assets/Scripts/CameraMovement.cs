/*
 * ¿Qué haremos?
 * 1.- NORMALIZAR VARIABLES -> Usamos un estandar u otro pero no combinados. Nosotros usaremos Camel Caes.
 * 2.- ESCRIBIR BIEN EL NOMBRE DE LAS VARIABLES -> No nos de miedo a tener nombres largos.
 * 3.- HACER CONSTANTES -> Hay dos valores que se asignan y no se modifican que son el horizontal y el vertical.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    /*[FormerlySerializeAs( "movement_speed" )]*/ [Range(1.0f, 20.0f)]
    public float movement_speed;

    [Range(1.0f, 6.0f)]
    public float distance;

    [Range(1.0f, 90.0f)]
    public float angle;

    private const float HorizontalScreenPercentage = 0.1f;
    private const float VerticalScreenPercentage = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        CenterAtPlayer();
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            CenterAtPlayer();
    }

    private void MoveCamera() {
        Vector3 mp = Input.mousePosition;
        int w = Screen.currentResolution.width;
        int h = Screen.currentResolution.height;


        Debug.Log(mp.x + " - " + mp.y);
        // Horizontal
        if (mp.x < w * HorizontalScreenPercentage) {
            transform.position -= new Vector3(0,0,1) * movement_speed * Time.deltaTime;
        }
        else if (mp.x > w - w * HorizontalScreenPercentage)
        {
            transform.position += new Vector3(0, 0, 1) * movement_speed * Time.deltaTime;
        }
        
        // Vertical
        if (mp.y < h * VerticalScreenPercentage)
        {
            transform.position += new Vector3(1, 0, 0) * movement_speed * Time.deltaTime;
        }
        else if (mp.y > h - h * VerticalScreenPercentage)
        {
            transform.position -= new Vector3(1, 0, 0) * movement_speed * Time.deltaTime;
        }
    }


    public void CenterAtPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float angleRad = Mathf.Deg2Rad * (90 - angle);

        float y = Mathf.Cos(angleRad) * distance;
        float x = Mathf.Sin(angleRad) * distance;


        float h = distance / Mathf.Sqrt(2);
        transform.position = player.transform.position + new Vector3(x, y, 0);
        transform.LookAt(player.transform);
    }
}
