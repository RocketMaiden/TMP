using UnityEngine;

public class FieldHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _nodePrefab = null;

    private Node[,] _field;
    private int _width;
    private int _height;

    public int Width => _width;
    public int Height => _height;
    public Node[,] Field => _field;

    // Start is called before the first frame update
    public void Initialize(int width, int height)
    {
        _width = width;
        _height = height;
        _field = new Node[_width, _height];
        
        GenerateField();
    }

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
                var go = Instantiate(_nodePrefab, new Vector3(i + i * 0.1f, 0f, j + j * 0.1f), Quaternion.identity);
                go.transform.SetParent(this.transform);

                var node = go.GetComponent<Node>();
                _field[i, j] = node;
            }
        }
        ClearNodeType();
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
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_field[i, j].NodeType == from)
                {
                    SetNode(_field[i, j], to);                    
                }
            }
        }
    }

    private void ClearNodeType()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                SetNode(_field[i, j], NodeType.Passable);
            }
        }
    }  

    

}
