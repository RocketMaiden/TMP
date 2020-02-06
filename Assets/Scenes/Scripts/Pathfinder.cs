using UnityEngine;

namespace Assets.Scenes.Scripts
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField]
        private Field _field;

        [SerializeField]
        private Path _path;


        private void Start()
        {            
            if(TryFindStartNode(out var node))
            {
                Debug.Log(node.transform.position);
                _path.Road[0] = node.transform.position;
            }
            else
            {
                Debug.LogError("start not found!!!");
            }

        }
        
        private void Update()
        {
            SetPath();
        }

        private bool TryFindStartNode(out Node node)
        {
            for (int i = 0; i < _field.width; i++)
            {
                for (int j = 0; j < _field.width; j++)
                {
                    if (_field.field[i, j].NodeType == NodeType.Start)
                    {
                        node = _field.field[i, j];
                        return true;
                    }
                }
            }
            node = null;
            return false;
        }
        

        private void SetPath()
        {
            for (int i = 0; i < _field.width; i++)
            {
                for (int j = 0; j < _field.width; j++)
                {
                    if (_field.field[i, j].NodeType == NodeType.Passable)
                    {
                       AddNodeToRoad(_field.field[i, j]);
                    }
                }
            }
        }
        public void AddNodeToRoad(Node node)
        {
            Vector3 nodePosition = node.transform.position;
            _path.Road.Add(nodePosition);
        }

    }
}
