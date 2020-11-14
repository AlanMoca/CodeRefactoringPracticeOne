using UnityEngine;
using UnityEngine.AI;

public class LOL_PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Transform _body;
    private Animator _ac;

    [SerializeField] private SpellConfiguration[] spells;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _body = transform.GetChild(0);
        _ac = _body.GetComponent<Animator>();
    }

    void Start()
    {
        _nav.updateRotation = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                _nav.SetDestination(hit.point);
        }

        foreach ( var spellConfiguration in spells )
        {
            HandleSpell( spellConfiguration );
        }

        _ac.SetBool("Run", IsMoving());

    }

    private void LateUpdate()
    {
        if(IsMoving()) transform.rotation = Quaternion.LookRotation(_nav.velocity.normalized);
    }

    private bool IsMoving() { return (_nav.velocity != Vector3.zero); }

    private void OnDrawGizmos()
    {
        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayAtk, out hitAtk, Mathf.Infinity))
        {
            Gizmos.DrawSphere(hitAtk.point, 0.025f);
        }
    }

    private void HandleSpell( SpellConfiguration spellConfiguration )
    {
        if (Input.GetKey( spellConfiguration.KeyCode ))
        {
            spellConfiguration.Spell.KeyPressed( transform );
            return;
        }

        if (Input.GetKeyUp( spellConfiguration.KeyCode ) )
        {
            spellConfiguration.Spell.KeyReleased( transform );
            return;
        }
        
        spellConfiguration.Spell.Reset();

    }

}
