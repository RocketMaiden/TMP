using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private FieldHolder _fieldHolder = null;

    [SerializeField]
    private Path _path = null;

    [SerializeField]
    private Button _button = null;

    // Start is called before the first frame update
    void Start()
    {            
        _fieldHolder.Initialize(4, 5);       
        _button.onClick.AddListener(FindPath);
    }

    // Update is called once per frame
    void Update()
    {
        _fieldHolder.Tick();
        _path.Tick();
    }

    private void FindPath()
    {
        
        Node nodeStart;
        Node nodeFinish;

        if (!TryFindFirstNode(NodeType.Start, out nodeStart))
        {
            Debug.Log("Start does not exist");
            return;
        }

        if (!TryFindFirstNode(NodeType.Finish, out nodeFinish))
        {
            Debug.Log("Finish does not exist");
            return;
        }

        var _localPath = new List<Vector3>();
        _localPath.Add(nodeStart.gameObject.transform.position);

        if (!TryFindPath(_localPath))
        {
            Debug.Log("There are no passable nodes!");
            return;
        }
        _localPath.Add(nodeFinish.gameObject.transform.position);

        _path.Initialize(_localPath);
    }    
    
    private bool TryFindFirstNode(NodeType type, out Node node)
    {
        for (int i =0; i < _fieldHolder.Width; i++)
        {
            for (int j = 0; j < _fieldHolder.Height; j++)
            {
                if (_fieldHolder.Field[i, j].NodeType == type)
                {
                    node = _fieldHolder.Field[i, j];
                    return true;
                }
            }
        }
        node = null;
        return false;
    }

    private bool TryFindPath(List <Vector3> path)
    {       
        for (int i = 0; i < _fieldHolder.Width; i++)
        {
            for (int j = 0; j < _fieldHolder.Height; j++)
            {
                if (_fieldHolder.Field[i, j].NodeType == NodeType.Passable)
                {
                    path.Add(_fieldHolder.Field[i, j].gameObject.transform.position);                    
                }
            }
        }       
        return true;
    }
    
}
