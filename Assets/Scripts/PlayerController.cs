using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private InputController inputController;

    private void Start()
    {
        inputController.OnDoubleTap += OnDoubleTap;
    }

    private void OnDoubleTap(Vector3 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Workplace workplace = hit.collider.GetComponent<Workplace>();
            if (workplace)
            {
                agent.SetDestination(workplace.WorkPosition.position);
            }
            else
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}