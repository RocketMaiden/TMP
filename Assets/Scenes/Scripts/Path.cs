using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;

    private List<Vector3> _path;
    private Vector3 _start;
    private Vector3 _target;
    private int _currentIndex;
    private float _movementSpeed;    

    public void Initialize(Vector3 start)
    {
        _start = start;
        _target = start;
        _currentIndex = 0;
        _movementSpeed = 3.5f;
    }

    public void SetRoad(List<Vector3> path)
    {
        _path = path;
    }
    
    public void Tick()
    { 
        if(_path == null || _path.Count < 2)
        {
            return;
        }
        if (Vector3.Distance(_start, _target) > 0.1f)
        {
            _start = Vector3.MoveTowards(_start, _target, _movementSpeed * Time.deltaTime);
            _player.transform.position = _start;
        }
        else
        {
            _currentIndex++;
            if (_currentIndex >= _path.Count)
            {
                _currentIndex = 0;
            }
            _target = _path[_currentIndex];
        }
    }    
    

}
