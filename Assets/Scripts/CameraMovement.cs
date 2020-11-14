/*
 * ¿Qué haremos?
 * 1.- NORMALIZAR VARIABLES -> Usamos un estandar u otro pero no combinados. Nosotros usaremos Camel Caes.
 * 2.- ESCRIBIR BIEN EL NOMBRE DE LAS VARIABLES -> No nos de miedo a tener nombres largos.
 * 3.- HACER CONSTANTES -> Hay dos valores que se asignan y no se modifican que son el horizontal y el vertical.
 * 4.- INDICAR MODIFICADORES CORRECTAMENTE -> Un código más limpio tiene bien especificado todo. (Private, public).
 * 5.- BORRAR COMENTARIOS QUE NO APORTEN NADA -> Como los de unity.
 * 6.- LIMPIAR INTERPRETACIONES -> Aquí tenemos que leer y pensar qué está haciendo el código y eso no es lo que queremos por ejemplo el if del update. Limpiaremos eso.
 * 7.- SIMPLIFICACION DE TIPOS CON VAR -> Dentro de los metodos no es necesario especificar el tipo de una variable ya que saber el tipo solo aporta ruido y ya está implicito en el nombre de la variable.
 * = Volvemos a modificar nomsbres de las variables para que sea explicito. =
 * 
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

    private void Start()
    {
        CenterAtPlayer();
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void Update()
    {
        if ( HasToCenterCameraAtPlayer() )
            CenterAtPlayer();
    }

    private static bool HasToCenterCameraAtPlayer()
    {
        return Input.GetKey( KeyCode.Space );
    }

    private void MoveCamera() {
        var mousePosition = Input.mousePosition;
        var currentResolutionWidth = Screen.currentResolution.width;
        var currentResolutionHeight = Screen.currentResolution.height;


        Debug.Log(mousePosition.x + " - " + mousePosition.y);
        // Horizontal
        if (mousePosition.x < currentResolutionWidth * HorizontalScreenPercentage) {
            transform.position -= new Vector3(0,0,1) * movement_speed * Time.deltaTime;
        }
        else if (mousePosition.x > currentResolutionWidth - currentResolutionWidth * HorizontalScreenPercentage)
        {
            transform.position += new Vector3(0, 0, 1) * movement_speed * Time.deltaTime;
        }
        
        // Vertical
        if (mousePosition.y < currentResolutionHeight * VerticalScreenPercentage)
        {
            transform.position += new Vector3(1, 0, 0) * movement_speed * Time.deltaTime;
        }
        else if (mousePosition.y > currentResolutionHeight - currentResolutionHeight * VerticalScreenPercentage)
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
