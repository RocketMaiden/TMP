using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _nodePrefab = null;

    public int Width => _width;
    public int Height => _height;
    public Node[,] Field => _field;


    private Node[,] _field;
    private int _width;
    private int _height;
    
    // Start is called before the first frame update
    public void Initialize(int width, int height)
    {
        _width = width;
        _height = height;
        _field = new Node[_width, _height];
        GenerateField();
    }

    // Update is called once per frame
    public void Tick() 
    {
        InputHandler();
    }

    private void GenerateField()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var go = Instantiate(_nodePrefab, new Vector3(i + i * 0.1f, j + j * 0.1f, 0f), Quaternion.identity);
                go.transform.SetParent(transform);
                _field[i, j] = go.GetComponent<Node>();
                _field[i, j].X = i;
                _field[i, j].Y = j;
                _field[i, j].NodeType = NodeType.Passable;
            }
        }
    }
    public void ClearField()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _field[i, j].ParentNode = null;
            }
        }
    }

    private void InputHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                var currentCube = hitInfo.collider.gameObject.GetComponent<Node>();
                if (currentCube != null)
                {
                    SetNode(currentCube, NodeType.Passable);                    
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                var currentCube = hitInfo.collider.gameObject.GetComponent<Node>();
                if (currentCube != null)
                {
                    SetNode(currentCube, NodeType.Impassable);                    
                }
            }
        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                var currentCube = hitInfo.collider.gameObject.GetComponent<Node>();
                if (currentCube != null)
                {
                    ChangeNodes(NodeType.Start, NodeType.Passable);
                    SetNode(currentCube, NodeType.Start);                    
                }
            }
        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                var currentCube = hitInfo.collider.gameObject.GetComponent<Node>();
                if (currentCube != null)
                {
                    ChangeNodes(NodeType.Finish, NodeType.Passable);
                    SetNode(currentCube, NodeType.Finish);                    
                }
            }
        }


    }   

    private void ChangeNodes(NodeType from, NodeType to)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {                
                if (_field[i,j].NodeType == from)
                {                    
                    SetNode(_field[i, j], to);
                }
            }
        }
    }

    private void SetNode(Node selectedNode, NodeType type)
    {
        selectedNode.NodeType = type;
        switch (type)
        {
            case NodeType.Start:
                selectedNode.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case NodeType.Finish:
                selectedNode.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case NodeType.Passable:
                selectedNode.gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case NodeType.Impassable:
                selectedNode.gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;
        }
    }

}
