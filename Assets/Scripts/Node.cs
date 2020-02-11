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
    public int X;
    public int Y;
    public Node ParentNode;

}
