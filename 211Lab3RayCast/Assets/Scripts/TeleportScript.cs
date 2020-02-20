using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [Header("Script References")]
    public TeleportRecieveScript TeleR;

    [Header("Coordinates for Teleport")]
    public Vector2 TeleportTo;

    [Header("To Endable/Disable Teleport")]
    public bool Sending;
    public bool Recieving;
    public int teleCount;

    [Header("Player Detection")]
    public bool hasLeft;
    public bool isHere;

    private void Start()
    {
        isHere = true;
        //Sending = true;
        //teleCount = 0;
    }

    private void Update()
    {
        if(hasLeft && isHere)//if the player IS at this spot 
        {
            teleCount = 0;
        }

        if(!isHere)//if the player is NOT at this spot
        {
            teleCount = 1;
        }
        if (teleCount == 0)
        {
            Recieving = false;
            Sending = true;
        }
        if(teleCount == 1)
        {
            Sending = false;
            Recieving = true;
        }
       if(Sending)
        {
            gameObject.layer = 0;//sets layer to default
        }
        if (Recieving)
        {
            gameObject.layer = 2;//sets layer to IgnoreRayCast
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasLeft = false;
            isHere = true;
            TeleR.isHere = false;
            TeleR.hasLeft = false;
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
