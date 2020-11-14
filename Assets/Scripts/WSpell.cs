using UnityEngine;
using UnityEngine.AI;

public class WSpell : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Transform _body;
    private Animator _ac;

    [SerializeField] private GameObject castPreviewRange;
    [SerializeField] private GameObject wSpellPreview;
    [SerializeField] private GameObject wSpell;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _body = transform.GetChild( 0 );
        _ac = _body.GetComponent<Animator>();
    }

    public void Reset()
    {
        wSpellPreview.SetActive( false );
        castPreviewRange.SetActive( false );
    }

    public void KeyPressed( Transform transform )
    {
        wSpellPreview.SetActive( true );
        castPreviewRange.SetActive( true );

        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay( Input.mousePosition );
        if ( Physics.Raycast( rayAtk, out hitAtk, Mathf.Infinity ) )
        {
            wSpellPreview.transform.position = hitAtk.point + new Vector3( 0, 0.02f, 0 );

        }
    }

    public void KeyReleased( Transform transform )
    {
        _nav.velocity = Vector3.zero;
        _nav.ResetPath();
        _ac.Play( "Spell_W" );

        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay( Input.mousePosition );
        if ( Physics.Raycast( rayAtk, out hitAtk, Mathf.Infinity ) )
        {
            float transformHeight = transform.position.y;
            float floorHeight = hitAtk.point.y;
            Vector3 point = new Vector3( hitAtk.point.x, floorHeight + 0.01f, hitAtk.point.z );

            transform.LookAt( point );

            GameObject spell = Object.Instantiate( wSpell, point, Quaternion.identity );
        }
    }
}
