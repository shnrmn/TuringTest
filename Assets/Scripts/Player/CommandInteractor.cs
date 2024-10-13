using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private Camera _cam;

    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;

    public override void Interact()
    {
        if (_agent == null)
            return;

        if (_input.CommandPressed)
        {
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    GameObject pointer = Instantiate(_pointerPrefab);
                    pointer.transform.position = hitInfo.point;
                    _commands.Enqueue(new MoveCommand(_agent, pointer.transform.position));
                }
                else if (hitInfo.transform.CompareTag("Builder"))
                    _commands.Enqueue(new BuildCommand(_agent, hitInfo.transform.GetComponent<Builder>()));
            }
        }
        ProcessCommands();
    }

    private void ProcessCommands()
    {
        if (_currentCommand != null && !_currentCommand.IsComplete)
            return;

        if (_commands.Count == 0)
            return;

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }
}
