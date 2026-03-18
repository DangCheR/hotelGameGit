using UnityEngine;
using UnityEngine.AI;

class PlayerControllor : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public LineRenderer LineRenderer;
    private Vector3[] _pathPositions;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _navMeshAgent.SetDestination(hit.point);
            }
        }
        if (_navMeshAgent.hasPath)
        {
            if (GetPath())
            {
                DrawLineRenderer(_pathPositions);
            }
        }
        else
        {
            LineRenderer.positionCount = 0;
        }
    }

    public bool GetPath()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        _pathPositions = _navMeshAgent.path.corners;
        return _pathPositions != null && _pathPositions.Length > 0;
    }

    public void DrawLineRenderer(Vector3[] positions)
    {
        LineRenderer.positionCount = positions.Length;
        LineRenderer.SetPositions(positions);
    }
}