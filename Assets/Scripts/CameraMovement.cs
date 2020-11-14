using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float HorizontalScreenPercentage = 0.1f;
    private const float VerticalScreenPercentage = 0.1f;

    private GameObject player;

    [Range( 1.0f, 20.0f )]
    public float movementSpeed;

    [Range( 1.0f, 6.0f )]
    public float distance;

    [Range( 1.0f, 90.0f )]
    public float angle;

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
        float angleRad = Mathf.Deg2Rad * ( 90 - angle );

        float yDistanceToTranslate = Mathf.Cos( angleRad ) * distance;
        float xDistanceToTranslate = Mathf.Sin( angleRad ) * distance;

        var newPosition = player.transform.position + new Vector3( xDistanceToTranslate, yDistanceToTranslate, 0 );
        transform.position = newPosition;
        transform.LookAt( player.transform );
    }
}