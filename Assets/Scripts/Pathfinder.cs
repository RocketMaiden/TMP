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
      

        if (!TryFindFirstNode(NodeType.Start, out nodeStart))
        {
            Debug.Log("Start does not exist");
            return;
        }
        Point _currentPoint = new Point();
        _currentPoint.X = nodeStart.X;
        _currentPoint.Y = nodeStart.Y;
       
        Node nodeFinish = GoFindPath(_currentPoint);
        if (nodeFinish == null)
        {
            Debug.Log("Finish does not exist");
            return;
        }

        var _localPath = new List<Vector3>();
        while (true)
        {
            _localPath.Add(nodeFinish.transform.position);
            if (nodeFinish.ParentNode == null)
            {               
                break;
            }            
            nodeFinish = nodeFinish.ParentNode;
        }
        _localPath.Reverse();
        _path.Initialize(_localPath);
        _fieldHolder.ClearField();
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
    struct Point
    {
        public int X;
        public int Y;
    }

    private Node GoFindPath(Point current)
    {
        List<Point> _surroundPoints = new List<Point>();

        Point rightNeighbour = new Point();
        rightNeighbour.X = current.X;
        rightNeighbour.Y = current.Y + 1;
        if (IsItSurround(rightNeighbour, current))
        {
            if (_fieldHolder.Field[rightNeighbour.X, rightNeighbour.Y].NodeType == NodeType.Finish)
            {
                return _fieldHolder.Field[rightNeighbour.X, rightNeighbour.Y];

            }
            _surroundPoints.Add(rightNeighbour);
        }

        Point leftNeighbour = new Point();
        leftNeighbour.X = current.X;
        leftNeighbour.Y = current.Y - 1;
        if (IsItSurround(leftNeighbour, current))
        {
            if (_fieldHolder.Field[leftNeighbour.X, leftNeighbour.Y].NodeType == NodeType.Finish)
            {
                return _fieldHolder.Field[leftNeighbour.X, leftNeighbour.Y];

            }
            _surroundPoints.Add(leftNeighbour);
        }

        Point upNeighbour = new Point();
        upNeighbour.X = current.X - 1;
        upNeighbour.Y = current.Y;
        if (IsItSurround(upNeighbour, current))
        {
            if (_fieldHolder.Field[upNeighbour.X, upNeighbour.Y].NodeType == NodeType.Finish)
            {
                return _fieldHolder.Field[upNeighbour.X, upNeighbour.Y];

            }
            _surroundPoints.Add(upNeighbour);
        }

        Point downNeighbour = new Point();
        downNeighbour.X = current.X + 1;
        downNeighbour.Y = current.Y;
        if (IsItSurround(downNeighbour, current))
        {
            if (_fieldHolder.Field[downNeighbour.X, downNeighbour.Y].NodeType == NodeType.Finish)
            {
                return _fieldHolder.Field[downNeighbour.X, downNeighbour.Y];

            }
            _surroundPoints.Add(downNeighbour);
        }


        for (var index = 0; index < _surroundPoints.Count; index++ )
        {
            var node = GoFindPath(_surroundPoints[index]);
            if (node != null)
            {
                return node;
            }
        }
        return null;
    }
    private bool IsItSurround(Point point, Point current)
    {
        if (IsPointValid(point))
        {
           
            _fieldHolder.Field[point.X, point.Y].ParentNode = _fieldHolder.Field[current.X, current.Y];
            return true;
        }
        return false;
    }



    private bool IsPointValid(Point point)
    {

        if (point.X < 0 || point.X >= _fieldHolder.Width ||
            point.Y < 0 || point.Y >= _fieldHolder.Height)
        {
            return false;
        }
        if ((_fieldHolder.Field[point.X, point.Y].NodeType == NodeType.Impassable) || (_fieldHolder.Field[point.X, point.Y].NodeType == NodeType.Start))
        {
            return false;
        }
        if (_fieldHolder.Field[point.X, point.Y].ParentNode != null)
        {
            return false;
        }
        return true;
    }

}
