/*
 * What will we do?
 * 1.- NORMALIZE VARIABLES -> We use one standard or another but not combined. We will use Camel Caes.
 * 2.- WRITE THE NAME OF THE VARIABLES WELL -> We are not afraid of having long names.
 * 3.- MAKE CONSTANTS -> There are two values ​​that are assigned and are not modified, which are horizontal and vertical.
 * 4.- INDICATE MODIFIERS CORRECTLY -> A cleaner code has everything well specified. (Private, public).
 * 5.- DELETE COMMENTS THAT DO NOT CONTRIBUTE ANYTHING -> Like those of unity.
 * 6.- CLEAN INTERPRETATIONS -> Here we have to read and think about what the code is doing and that is not what we want, for example the if of the update. We will clean that up.
 * 7.- TYPES SIMPLIFICATION WITH VAR -> Within the methods it is not necessary to specify the type of a variable since knowing the type only contributes noise and is already implicit in the name of the variable.
 * = We change the names of the variables again to make it explicit. =
 * 8.- AVOID COMMENTS. Comments don't fix bad code -> Extract horizontal movement and make it more readable.
 * 9.- OPTIMIZATION WITHIN THE EXTRACTION -> It is modified how the vectors are being multiplied so that they multiply scalars together and instead of creating new vectors use the predefined vectors.
 * = More extraction for more readability =
 * 10.- MODIFICATION OF CONDITIONS -> It is the same as cleaning interpretations. We will do it within the extractions.
 * 11.- SAVE CLAUSE -> It is an optimization that is applied to the if's statements and we will apply it to the ones we have just extracted.
 * = More Optimization and standardization of variables = -> FindObjectType.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    [Range(1.0f, 20.0f)]
    public float movementSpeed;

    [Range(1.0f, 6.0f)]
    public float distance;

    [Range(1.0f, 90.0f)]
    public float angle;

    private GameObject player;

    private const float HorizontalScreenPercentage = 0.1f;
    private const float VerticalScreenPercentage = 0.1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag( "Player" );
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

    public void CenterAtPlayer()
    {
        float angleRad = Mathf.Deg2Rad * (90 - angle);

        float yDistanceToTranslate = Mathf.Cos(angleRad) * distance;
        float xDistanceToTranslate = Mathf.Sin(angleRad) * distance;

        var newPosition = player.transform.position + new Vector3(xDistanceToTranslate, yDistanceToTranslate, 0);
        transform.position = newPosition;
        transform.LookAt(player.transform);
    }
}
