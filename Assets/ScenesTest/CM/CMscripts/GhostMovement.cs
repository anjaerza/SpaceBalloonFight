// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] string side;
   public string Side { get => side; set => side = value; }
  bool masde1080;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // if (player.transform.position.x > transform.position.x)
        // {
        //     side = "Left";
        // }

        // if (player.transform.position.x < transform.position.x)
        // {
        //     side = "Right";
        // }
         if(Screen.width>1080 || Screen.height>2560)
          {
              masde1080=true;
          }
        if(Screen.width<=1080||Screen.height<=2560)
        {
           masde1080=false;
        }
    }

    private void Update()
    {
      //  print("ResPantalla : " +player.GetComponent<CMScreenWrapRF>().ScreenWidth );
       
        if (side == "Left")
        {
            Vector3 newPos = new Vector3();
          if(masde1080==true)
          {
              // print("Entro a mas de 1080 ");
            newPos.x = player.transform.position.x - player.GetComponent<CMScreenWrapRF>().ScreenWidth/**0.98f*/;
            newPos.y = player.transform.position.y;
            newPos.z = -0.52f;

            transform.position = newPos;
          }
        if(masde1080==false)
          {
          //  print("Entro a menos de 1080 ");
            newPos.x = player.transform.position.x - player.GetComponent<CMScreenWrapRF>().ScreenWidth/**0.85f*/;
            newPos.y = player.transform.position.y;
            newPos.z = -0.52f;

            transform.position = newPos;
          }
           
        }

        if (side == "Right")
        {

          if(masde1080==true)
          {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x + player.GetComponent<CMScreenWrapRF>().ScreenWidth/**0.98f*/;
            newPos.y = player.transform.position.y;
            newPos.z = -0.52f;

            transform.position = newPos;
          }
          if(masde1080==false)
          { 
             Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x + player.GetComponent<CMScreenWrapRF>().ScreenWidth/**0.85f*/;
            newPos.y = player.transform.position.y;
            newPos.z = -0.52f;

            transform.position = newPos;
          }
        }
          if (side == "Up")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x;
            newPos.y = player.transform.position.y + player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
          if (side == "Down")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x ;
            newPos.y = player.transform.position.y - player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
          if (side == "UpRig")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x + player.GetComponent<CMScreenWrapRF>().ScreenWidth;
            newPos.y = player.transform.position.y + player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
          if (side == "UPLeft")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x - player.GetComponent<CMScreenWrapRF>().ScreenWidth;
            newPos.y = player.transform.position.y+player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
          if (side == "DownRigh")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x + player.GetComponent<CMScreenWrapRF>().ScreenWidth;
            newPos.y = player.transform.position.y-player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
          if (side == "DownLeft")
        {
            Vector3 newPos = new Vector3();
            newPos.x = player.transform.position.x - player.GetComponent<CMScreenWrapRF>().ScreenWidth;
            newPos.y = player.transform.position.y -  player.GetComponent<CMScreenWrapRF>().ScreenHeight;
            newPos.z = -0.52f;

            transform.position = newPos;
        }
    }
}
