/*
 * 12.- OPEN-CLOSED & SINGLE RESPONSABILITY -> Extraeremos la gestión de habilidades. (Q y W buttons).
 * 12.1.- Creamos clases para extracción de habilidades.
 * NOTA: No se extraen las teclas que usamos para la hbilidad, la habilidad sólo se preocupa de sí misma. (Así cumplimos el primer principio).
 * NOTA: Nombre de objeto en la vida real para las clases y nombre de verbo para los métodos.
 * 12.2.- Resolver las variables que necesitan -> En vez de hacer el transform Monobehaviour, se lo pasamos por parametro al método. Y al instantiate le haremos: Object.Instantiate()... Necesitamos serealizar por lo que haremos las clases monobehaviour
 * 12.3.- Se agregan las instancias a la clase en la clase PlayerMovement y se llaman a sus métodos donde se presionan las teclas. Y aplicamos regla de clausula de guarda a los if de la tecla Q, para quitar parentesis y niveles de anidamiento.
 * 12.4.- Se aplica lo mismo para la clase WSpell.
 * 12.5.- En este punto el código queda más compacto y limpio, además que extrajimos 2 responsabilidades de la clase.
 * 12.6.- La clase LOL_PlayerMovement estaba moviendose y aplicando las habilidades. El nombre no era consistente con las responsabilidades que tenía y sigue sin serlo porque aunque no tiene el behaviour si da las ordenes de cuando ejecutarse.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LOL_PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Transform _body;    // Body gameobject HAS to be the first child in the list
    private Animator _ac;

    [SerializeField] private QSpell qSpell;
    [SerializeField] private WSpell wSpell;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _body = transform.GetChild(0);
        _ac = _body.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                _nav.SetDestination(hit.point);
        }


        HandleQAttack();
        HandleWAttack();


        
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

    private void HandleQAttack()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            qSpell.KeyPressed( transform );
            return;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            qSpell.KeyReleased( transform );
            return;
        }
        
        qSpell.Reset();

    }

    private void HandleWAttack()
    {
        if (Input.GetKey(KeyCode.W))
        {
            wSpell.KeyPressed( transform );
            return;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            wSpell.KeyReleased( transform );
            return;
        }
        
        wSpell.Reset();

    }
}
