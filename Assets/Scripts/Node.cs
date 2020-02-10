using UnityEngine;


public enum NodeType
{
    Passable,
    Impassable,
    Start,
    Finish,
    None
}
public class Node : MonoBehaviour
{
    public NodeType NodeType;
}
