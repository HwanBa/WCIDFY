// - Made by HwanBa
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
// - Member Variable
    private Vector3 _movedir;
    private NavMeshAgent _navMeshAgent;


// - Method
    // - base
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
    }

    void Update()
    {
        _movedir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _movedir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movedir -= Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _movedir -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _movedir += Vector3.right;
        }

        if( _movedir != Vector3.zero)
        {
            Vector3 movenormal = _movedir.normalized;
            _navMeshAgent.Move(movenormal * _navMeshAgent.speed * Time.deltaTime);
        }
    }

    // - origin
}
