using UnityEngine;
public enum NodeType
{    
    Passable,
    ImPassable,
    Start,
    Finish,
    None
}

public class Node : MonoBehaviour
{
    public NodeType NodeType = NodeType.None;
}
