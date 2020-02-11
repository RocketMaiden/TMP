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
                    node.X = i;
                    node.Y = j;
                    node.ParentNode = null;
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


    struct Point
    {
        public int X;
        public int Y;
    }

    private void PathFindingAlgorythm()
    {
        //todo save parent if it valise
        Node _current;
        List<Node> _path = new List<Node>();        


        if (TryFindFirstNode(NodeType.Start, out _current))
        {
            Point _currentPoint = new Point();
            _currentPoint.X = _current.X;
            _currentPoint.Y = _current.Y;
            GoFindPath(_currentPoint);
        }      
        
    }

    private void RevealThePath(List<Point> visitedPoints)
    {
        List<Node> _path = new List<Node>();
        for (int i = 0; i <= visitedPoints.Count; i++)
        {
            _path.Add(_fieldHolder.Field[visitedPoints[i].X, visitedPoints[i].Y].ParentNode);
        }
    }
    
    private void GoFindPath(Point current)
    {
        List<Point> _surroundPoints = new List<Point>();

        Point rightNeighbour = new Point();
        rightNeighbour.X = current.X;
        rightNeighbour.Y = current.Y + 1;

        Point leftNeighbour = new Point();
        leftNeighbour.X = current.X;
        leftNeighbour.Y = current.Y - 1;

        Point upNeighbour = new Point();
        upNeighbour.X = current.X - 1;
        upNeighbour.Y = current.Y;

        Point downNeighbour = new Point();
        downNeighbour.X = current.X + 1;
        downNeighbour.Y = current.Y;

        if (IsPointValid(rightNeighbour))
        {
            if (_fieldHolder.Field[rightNeighbour.X, rightNeighbour.Y].NodeType == NodeType.Finish)
            {
                Debug.Log(rightNeighbour);
                return;
            }
            _fieldHolder.Field[rightNeighbour.X, rightNeighbour.Y].ParentNode = _fieldHolder.Field[current.X, current.Y];
            _surroundPoints.Add(rightNeighbour); 
        }
        if (IsPointValid(downNeighbour))
        {
            if (_fieldHolder.Field[downNeighbour.X, downNeighbour.Y].NodeType == NodeType.Finish)
            {
                Debug.Log (downNeighbour);
            }
            _fieldHolder.Field[downNeighbour.X, downNeighbour.Y].ParentNode = _fieldHolder.Field[current.X, current.Y];
            _surroundPoints.Add(downNeighbour);
        }
        if (IsPointValid(leftNeighbour))
        {
            if (_fieldHolder.Field[leftNeighbour.X, leftNeighbour.Y].NodeType == NodeType.Finish)
            {
                Debug.Log (leftNeighbour);
            }
            _fieldHolder.Field[leftNeighbour.X, leftNeighbour.Y].ParentNode = _fieldHolder.Field[current.X, current.Y];
            _surroundPoints.Add(leftNeighbour);
        }
        if (IsPointValid(upNeighbour))
        {
            if (_fieldHolder.Field[upNeighbour.X, upNeighbour.Y].NodeType == NodeType.Finish)
            {
                Debug.Log (upNeighbour);
            }
            _fieldHolder.Field[upNeighbour.X, upNeighbour.Y].ParentNode = _fieldHolder.Field[current.X, current.Y];
            _surroundPoints.Add(upNeighbour);
        }
        for(var index = 0; index < _surroundPoints.Count; index++ )
        {
            GoFindPath(_surroundPoints[index]);            
        }
        RevealThePath(_surroundPoints);
    }



    private bool IsPointValid(Point point)
    {   

        if ( !(0 <= point.X) && (point.X <= _fieldHolder.Width) && ( 0 <= point.Y) && (point.Y <= _fieldHolder.Height))
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
