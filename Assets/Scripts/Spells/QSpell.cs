using Spells;
using UnityEngine;
using UnityEngine.AI;

public class QSpell : Spell
{
    private NavMeshAgent _nav;
    private Transform _body;
    private Animator _ac;

    [SerializeField] private GameObject castPreviewRange;
    [SerializeField] private GameObject qSpellPreview;
    [SerializeField] private GameObject qSpell;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _body = transform.GetChild( 0 );
        _ac = _body.GetComponent<Animator>();
    }

    public override void Reset()
    {
        qSpellPreview.SetActive( false );
        castPreviewRange.SetActive( false );
    }

    public override void KeyPressed(Transform transform)
    {
        qSpellPreview.SetActive( true );
        castPreviewRange.SetActive( true );

        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay( Input.mousePosition );
        if ( Physics.Raycast( rayAtk, out hitAtk, Mathf.Infinity ) )
        {
            float floorHeight = hitAtk.point.y;
            Vector3 center = new Vector3( transform.position.x, floorHeight, transform.position.z );

            Vector3 dir = hitAtk.point - center;
            dir = new Vector3( dir.x, 0, dir.z );
            Vector3 castDir = qSpellPreview.transform.position - center;
            castDir = new Vector3( castDir.x, 0, castDir.z );
            float sAngle = Vector3.SignedAngle( dir, castDir, Vector3.up );

            //Debug.DrawRay(center, dir, Color.red);
            //Debug.DrawRay(center, castDir * 50, Color.blue);

            int sign = ( sAngle >= 0 ) ? 1 : -1;
            float angle = Mathf.Abs( sAngle );
            if ( angle > 0.3f )
                qSpellPreview.transform.RotateAround( center, Vector3.up, -sign * angle );



            Debug.Log( angle );
        }
    }

    public override void KeyReleased(Transform transform)
    {
        _nav.velocity = Vector3.zero;
        _nav.ResetPath();
        _ac.Play( "Spell_Q" );

        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay( Input.mousePosition );
        if ( Physics.Raycast( rayAtk, out hitAtk, Mathf.Infinity ) )
        {
            float transformHeight = transform.position.y;
            float floorHeight = hitAtk.point.y;
            Vector3 point = new Vector3( hitAtk.point.x, transformHeight, hitAtk.point.z );



            transform.LookAt( point );

            Vector3 center = new Vector3( transform.position.x, floorHeight, transform.position.z );

            Vector3 dir = hitAtk.point - center;
            dir = new Vector3( dir.x, 0, dir.z );
            GameObject spell = Object.Instantiate( qSpell, transform.position, Quaternion.identity );
            spell.GetComponent<Rigidbody>().velocity = dir.normalized * 5.0f;
            Debug.Log( dir.normalized );
        }
    }

}
