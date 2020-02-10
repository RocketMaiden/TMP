using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;

    [SerializeField]
    private float _speed = 0;

    private List<Vector3> _path;
    private Vector3 _start;
    private Vector3 _target;
    
    private int _index;


    public void Initialize(List<Vector3> path)
    {
        _start = path[0];
        _target = _start;
        _path = path;        
        _index = 0;

        _player.transform.position = _start;
    }
    public void Tick()
    {
        if ((_path == null) || (_path.Count < 2))
        {
            return;
        }        
        
        if (Vector3.Distance(_start, _target) > 0.1f)
        {
            _start = Vector3.MoveTowards(_start, _target, _speed*Time.deltaTime);
            _player.transform.position = _start;
        }
        else
        {
            _index++;
            if (_index >= _path.Count)
            {
                _index = 0;
            }
            _target = _path[_index];
        }
    }
}
