using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRotation : MonoBehaviour
{

    
    public AIPath aIPath;
    
    /// <summary>
    /// This metod is for rotate the enemi to the disired direction, if you need a diferent scale you need 
    /// to change a script or convert in a serializable component.
    /// </summary>
    void Update()
    {
       if(aIPath.desiredVelocity.x >=0.01f)
       {           
           transform.localScale = new Vector3(1f,1f,1f);
       }
       else if(aIPath.desiredVelocity.x <=-0.01f)
       {

           transform.localScale = new Vector3(-1f,1f,1);

       }
    }



}
