using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRecieveScript : MonoBehaviour
{
    [Header("Script References")]
    public TeleportScript TeleS;

    [Header("Coordinates for Teleport")] 
    public Vector2 SendTo;

    [Header("Enable/Disable Teleport")]
    public bool sending;
    public bool recieving;
    public int Telecount;

    [Header("Player Detection")]
    public bool hasLeft;
    public bool isHere;

    private void Start()
    {
        isHere = false;
        //recieving = true;
        //Telecount = 1;
    }
    void Update()
    {
        if (hasLeft && isHere)// if the player IS at this spot
        {
            Telecount = 0;
        }

        if(!isHere)//if the player is NOT at this spot
        {
            Telecount = 1;
        }

        if (Telecount == 1)
        {
            recieving = true;
            sending = false;
        }

        if(Telecount == 0)
        {
            recieving = false;
            sending = true;
        }

       if(sending)
        {
            gameObject.layer = 0;//sets layer to default 
        }

       if(recieving)
        {
            gameObject.layer = 2;//sets layer to ignoreRaycast
        }
      

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            hasLeft = false;
            isHere = true;
            TeleS.isHere = false;
            TeleS.hasLeft = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    { 
        if(collision.gameObject.tag == "Player")
        {
            hasLeft = true;
        }
    }
}
