using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private GameObject _nodePrefab = null;

    public Node[,] field;
    public int width = 6;
    public int height = 6;    

    // Start is called before the first frame update
    private void Start()
    {
        field = new Node[width, height];
        
        GenerateField();
    }

    private void Update()
    {
        InputHandler();        
    }

    private void InputHandler()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                var selectedCube = hitInfo.transform.gameObject.GetComponent<Node>();
                if (selectedCube != null)
                {
                    SetNode(selectedCube, NodeType.ImPassable);
                    Debug.Log("You have touched a nodeCube");
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {

                var selectedCube = hitInfo.transform.gameObject.GetComponent<Node>();
                if (selectedCube != null)
                {
                    SetNode(selectedCube, NodeType.Passable);
                    Debug.Log("You have touched a nodeCube");
                }
            }
        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {

                var selectedCube = hitInfo.transform.gameObject.GetComponent<Node>();
                if (selectedCube != null)
                {
                    ReplaceNodeType(NodeType.Start, NodeType.Passable);
                    SetNode(selectedCube, NodeType.Start);

                    Debug.Log("You have touched a nodeCube");
                }
            }
        }



        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {

                var selectedCube = hitInfo.transform.gameObject.GetComponent<Node>();
                if (selectedCube != null)
                {
                    ReplaceNodeType(NodeType.Finish, NodeType.Passable);
                    SetNode(selectedCube, NodeType.Finish);
                    Debug.Log("You have touched a nodeCube");
                }
            }
        }
    }

    private void SetNode(Node selectedCube, NodeType type)
    {
        selectedCube.NodeType = type;

        switch (selectedCube.NodeType)
        {
            case NodeType.Start:
                selectedCube.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case NodeType.Finish:
                selectedCube.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case NodeType.ImPassable:                
                selectedCube.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;
            case NodeType.Passable:
                selectedCube.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }

    private void ReplaceNodeType(NodeType from, NodeType to)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (field[i, j].NodeType == from)
                {
                    SetNode(field[i, j], to);                    
                }
            }
        }
    }

    private void ClearNodeType()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SetNode(field[i, j], NodeType.Passable);
            }
        }
    }   

    private void GenerateField()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var go = Instantiate(_nodePrefab, new Vector3(i + i * 0.1f, 0f, j + j * 0.1f), Quaternion.identity);
                go.transform.SetParent(this.transform);

                var node = go.GetComponent<Node>();
                field[i, j] = node;
            }
        }
        ClearNodeType();
    }

}
