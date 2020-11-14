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
 * 8.- EVITAR COMENTARIOS. Los comentarios no arreglan un mal código -> Extraer el movimiento horizontal y lo hacemos más legible.
 * 9.- OPTIMIZACION DENTRO DE LA EXTRACCION -> Se modifica como están multiplicando los vectores para que multiplique escalares juntos y en vez de crear vectores nuevos usar los vectores predefinidos.
 * = Más extraccion para más legibilidad =
 * 10.- MODIFICACION DE CONDICIONALES -> Es lo mismo que la limpieza de interpretaciones. Lo haremos dentro de las extracciones.
 * 11.- CASO DE GUARDA -> Es una optimización que se aplica a los if's statement y lo aplicaremos a los que acabamos de extraer.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    /*[FormerlySerializeAs( "movement_speed" )]*/ [Range(1.0f, 20.0f)]
    public float movementSpeed;

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

    private void MoveCamera()
    {
        var mousePosition = Input.mousePosition;
        var currentResolutionWidth = Screen.currentResolution.width;
        var currentResolutionHeight = Screen.currentResolution.height;

        Debug.Log( mousePosition.x + " - " + mousePosition.y );

        MoveCameraHorizontal( mousePosition, currentResolutionWidth );
        MoveCameraVertical( mousePosition, currentResolutionHeight );
    }

    private void MoveCameraVertical( Vector3 mousePosition, int currentResolutionHeight )
    {
        var isMousePositionAtTheBottom = mousePosition.y < currentResolutionHeight * VerticalScreenPercentage;
        if ( isMousePositionAtTheBottom )
        {
            ApplyTranslation( Vector3.right, movementSpeed );
            return;
        }

        var isMousePositionAtTheTop = mousePosition.y > currentResolutionHeight - currentResolutionHeight * VerticalScreenPercentage;
        if ( isMousePositionAtTheTop )
            ApplyTranslation( Vector3.left, movementSpeed );

    }

    private void MoveCameraHorizontal( Vector3 mousePosition, int currentResolutionWidth )
    {
        var isMousePositionAtTheLeft = mousePosition.x < currentResolutionWidth * HorizontalScreenPercentage;
        if ( isMousePositionAtTheLeft )
        {
            ApplyTranslation( Vector3.back, movementSpeed );
            return;
        }

        var isMousePositionAtTheRight = mousePosition.x > currentResolutionWidth - currentResolutionWidth * HorizontalScreenPercentage;
        if ( isMousePositionAtTheRight )
            ApplyTranslation( Vector3.forward, movementSpeed );

    }

    private void ApplyTranslation( Vector3 direction, float speed )
    {
        transform.position += direction * ( speed * Time.deltaTime );
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
