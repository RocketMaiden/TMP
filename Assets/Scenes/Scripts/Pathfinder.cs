using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.Scripts
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField]
        private FieldHolder _fieldHolder = null;

        [SerializeField]
        private Path _path = null;

        [SerializeField]
        private Button _goButton = null;


        private void Start()
        {
            _fieldHolder.Initialize(6, 6);
            _goButton.onClick.AddListener(FindPath);

        }
        
        private void Update()
        {
            _path.Tick();
            _fieldHolder.Tick();
        }

        private bool TryFindStartNode(out Node node)
        {
            for (int i = 0; i < _fieldHolder.Width; i++)
            {
                for (int j = 0; j < _fieldHolder.Height; j++)
                {
                    if (_fieldHolder.Field[i, j].NodeType == NodeType.Start)
                    {
                        node = _fieldHolder.Field[i, j];                        
                        return true;
                    }
                }
            }
            node = null;
            return false;
        }  
        


        private void FindPath()
        {
            if (TryFindStartNode(out var node))
            {
                Debug.Log(node.transform.position);
                _path.Initialize(node.transform.position);
            }
            else
            {
                Debug.LogError("start not found!!!");
                return;
            }

            List<Vector3> road = new List<Vector3>();

            for (int i = 0; i < _fieldHolder.Width; i++)
            {
                for (int j = 0; j < _fieldHolder.Width; j++)
                {
                    if (_fieldHolder.Field[i, j].NodeType == NodeType.Passable)
                    {
                        road.Add(_fieldHolder.Field[i, j].transform.position);
                    }
                }
            }
            _path.SetRoad(road);
        }        

    }
}
