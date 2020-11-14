/*
 * What will we do?
 * 1.- NORMALIZE VARIABLES -> We use one standard or another but not combined.We will use Camel Caes.

* 2.- WRITE THE NAME OF THE VARIABLES WELL -> We are not afraid of having long names.
 * 3.- MAKE CONSTANTS -> There are two values ​​that are assigned and are not modified, which are horizontal and vertical.

* 4.- INDICATE MODIFIERS CORRECTLY -> A cleaner code has everything well specified. (Private, public).
 * 5.- DELETE COMMENTS THAT DO NOT CONTRIBUTE ANYTHING -> Like those of unity.
 * 6.- CLEAN INTERPRETATIONS -> Here we have to read and think about what the code is doing and that is not what we want, for example the if of the update.We will clean that up.
 * 7.- TYPES SIMPLIFICATION WITH VAR -> Within the methods it is not necessary to specify the type of a variable since knowing the type only contributes noise and is already implicit in the name of the variable.
 * = We change the names of the variables again to make it explicit. =
 * 8.- AVOID COMMENTS. Comments don't fix bad code -> Extract horizontal movement and make it more readable.
 * 9.- OPTIMIZATION WITHIN THE EXTRACTION -> It is modified how the vectors are being multiplied so that they multiply scalars together and instead of creating new vectors use the predefined vectors.
 * = More extraction for more readability =
 *10.-
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
        if ( mousePosition.y < currentResolutionHeight * VerticalScreenPercentage )
        {
            ApplyTranslation( Vector3.right, movementSpeed );
        }
        else if ( mousePosition.y > currentResolutionHeight - currentResolutionHeight * VerticalScreenPercentage )
        {
            ApplyTranslation( Vector3.left, movementSpeed );
        }
    }

    private void MoveCameraHorizontal( Vector3 mousePosition, int currentResolutionWidth )
    {
        if ( mousePosition.x < currentResolutionWidth * HorizontalScreenPercentage )
        {
            ApplyTranslation( Vector3.back, movementSpeed );
        }
        else if ( mousePosition.x > currentResolutionWidth - currentResolutionWidth * HorizontalScreenPercentage )
        {
            ApplyTranslation( Vector3.forward, movementSpeed );
        }
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
