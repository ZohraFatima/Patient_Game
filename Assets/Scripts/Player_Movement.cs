using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 3f; // Speed variable
    
    private bool isMoving = false;
    public static Vector3 target;
   [SerializeField] private Camera maincamera;
   
    void Start()
    {
       
        //target = transform.position;
        
    }

    
    void Update()
    {
        

      
       if (Input.GetMouseButton(0))
        {
            // Convert mouse position to world coordinates
            Debug.Log("clicked");
            Vector3 mouseposition = Input.mousePosition;
            mouseposition.z = 10f;
            target = maincamera.ScreenToWorldPoint(mouseposition);
            target.z = 10f;
            isMoving = true;
            Debug.Log(Input.mousePosition);
            Debug.Log(target);
            
        }
        if(isMoving && transform.position != target)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,target,step);
        }
        else
        {
            isMoving= false;
        }


            
            
    }

   
}
