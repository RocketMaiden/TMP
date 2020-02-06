using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> Road;
    

    public Vector3 Position;
    public Vector3 Target;

    private int currentIndex;
    private float movementSpeed;

    private void Start()
    {
        
        //Position = Road[0];
        //Target = Road[1];
        currentIndex = 0;       

    }

    private void Update()
    {
        movementSpeed = 0.7f * Time.deltaTime;
        if (Vector3.Distance(Position, Target) > 0.1f)
        {
            Position = Vector3.MoveTowards(Position, Target, movementSpeed);
            transform.position = Position;
        }
        else
        {
            currentIndex++;
            if (currentIndex >= Road.Count)
            {
                currentIndex = 0;
            }            
            Target = Road[currentIndex];
        }
    }    
    

}
