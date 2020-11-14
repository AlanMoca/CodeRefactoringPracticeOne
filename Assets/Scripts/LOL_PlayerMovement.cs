/*
 * 12.- OPEN-CLOSED & SINGLE RESPONSABILITY -> We will extract skills management. (Q and W buttons).
 * 12.1.- We create classes for skills extraction.
 * NOTE: The keys that we use for the skill are not removed, the skill only cares about itself. (Thus we fulfill the first principle).
 * NOTE: Real life object name for classes and verb name for methods.
 * 12.2.- Solve the variables they need -> Instead of doing the Monobehaviour transform, we pass it as a parameter to the method. And to instantiate we will do: Object.Instantiate () ... We need to perform so we will do the monobehaviour classes
 * 12.3.- Instances are added to the class in the PlayerMovement class and their methods are called where the keys are pressed. And we apply the guard clause rule to the ifs of the Q key, to remove parentheses and nesting levels.
 * 12.4.- The same applies for the WSpell class.
 * 12.5.- At this point the code is more compact and clean, in addition we have extracted 2 responsibilities from the class.
 * 12.6.- The LOL_PlayerMovement class was moving and applying the skills. The name was not consistent with the responsibilities it had and it still is not because although it does not have the behavior, it does give the orders of when to execute.
 * 13.- Open-Closed -> We will use it to add new abilities and we will not have to modify the player every time we do it.
 * 13.1.- We will disappear the instances of the QSpell and WSpell classes and in their place we will generalize them so that they have a common parent.
 * NOTE: Today this would be ideal but unity still does not allow interfaces to be serialized. And we cannot get them by getcomponent in awake because they are 2 with the same interface, which causes a conflict that does not know which one to use.
 * 13.2.- We will change the interface to an abstract class and its references and instances.
 * NOTE: To pull it in the editor you pull it directly from the component you don't pull the gameObject.
 * 14.- VIEW AGGREGATION -> Now what we will do is that instead of having 2 Spells in the Getcomponents, we will assign a view to have all the ones we want. And the configuration of the key to each spell.
 * 14.1.- We will extract the key.
 * 14.2.- EXTRACTION OF CONFIGURATION CLASS -> We will make a class for the configuration of the spells, we will create an array for the spells and we will extract the handleAtack method in just one.
*/

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
