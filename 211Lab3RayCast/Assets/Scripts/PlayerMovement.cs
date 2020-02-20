using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Starting Position")]
    private Vector2 target;
    public Vector2 startPos;

    [Header("Script References")]
    public TeleportScript TS1;
    public TeleportScript TS2;
    public TeleportScript TS3;
    public TeleportRecieveScript TR1;
    public TeleportRecieveScript TR2;
    public TeleportRecieveScript TR3;


    [Header("Teleport Object References")]
    public GameObject Sender1;
    public GameObject Sender2;
    public GameObject Sender3;


    public GameObject Reciever1;
    public GameObject Reciever2;
    public GameObject Reciever3;




    [Header("Panel and UI References")]
    public GameObject PausePanel;
    public GameObject NextlevelPanel;
    public GameObject losePanel;
    public GameObject PlayerHUD;
    public Text Health;
    public Text CurrentLevel;
    public Text NumberCollected;
    private bool GamePaused = false;
    public int currentLevel;
    private int maxLevel = 4;

    [Header("Pause Menu References")]
    public Text PauseTotal;
    public Text Pauselevel;

    [Header("Win Panel References")]
    public Text winTotal;

    [Header("Lose Panel References")]
    public Text loseTotal;

    [Header("Health System References")]
    private int totalHealth = 1;
    private int currentHealth;
    private bool isDead;

    [Header("Win Condition")]
    public int WinTotal;
    public int currentTotal;
    private bool hasWon;

 

    // Start is called before the first frame update
    void Start()
    {
        target = startPos;
        currentHealth = totalHealth;

        //setting current level in HUD and pause menu
        CurrentLevel.text = ("Level: " + currentLevel + " / " + maxLevel);
        Pauselevel.text = ("Level: " + currentLevel + " / " + maxLevel);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement System
        Vector2 direction = Vector2.zero;

        bool notMoving = ((Vector2)transform.position == target);
        if(Input.GetKeyDown(KeyCode.P))
        {
            GamePaused = true;
            PausePanel.SetActive(true);
            PlayerHUD.SetActive(false);

        }//sets pause panel active and disables player movement if pressed

        if(!GamePaused)
        {
            if (Input.GetKeyDown(KeyCode.A) && notMoving)//moves player left if object is there and player is not already moving
            {
                direction = new Vector2(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && notMoving)//moves player right if object is there and player is not already moving
            {
                direction = new Vector2(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.W) && notMoving)//moves player up if object is there and player is not already moving
            {
                direction = new Vector2(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && notMoving)////moves player down if object is there and player is not already moving
            {
                direction = new Vector2(0, -1);
            }
            PlayerHUD.SetActive(true);
            PausePanel.SetActive(false);

        }//allows player movemnet if a panel is not active

       //simple health system
       if(currentHealth <= 0)
        {
            isDead = true;
        }//sets isDead true if current health <= 0. Means player is dead. 
       else
        {
            isDead = false;
        }//sets isDead false. Means player is dead

       if(isDead == true)
        {
            PlayerHUD.SetActive(false);
            losePanel.SetActive(true);
            GamePaused = true;
            Destroy(gameObject);
        }//activates losePanel and disables player movement

        //Raycasting
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        Debug.DrawLine(transform.position, transform.position + (Vector3)direction, Color.yellow);
        if(hit.collider != null)
        {
            if (hit.collider.tag == "Carrot" || hit.collider.tag == "Candy"||hit.collider.tag == "TeleCandy"||hit.collider.tag == "TeleReciever"|| hit.collider.tag == "TS2" || hit.collider.tag == "TS3" || hit.collider.tag == "TR2" || hit.collider.tag == "TR3")
            {
                target = hit.transform.position;
            }
         }

        transform.position = Vector2.Lerp(transform.position, target, 0.5f);
        

        //win condition
        if(currentTotal == WinTotal)
        {
            hasWon = true;
        }//sets hasWon bool to true 
        if(hasWon)
        {
            PlayerHUD.SetActive(false);
            NextlevelPanel.SetActive(true);
            GamePaused = true;
        }//if hasWon is true then sets nextLevelPanel true and disables player movement

        //updating Player health in HUD
        Health.text = ("Health: " + currentHealth + " / " + totalHealth);

        //updating total collected in HUD, pause menu, lose, and win panels
        NumberCollected.text = ("Candies Collected: " + currentTotal + " / " + WinTotal);
        PauseTotal.text = ("Candies Collected: " + currentTotal + " / " + WinTotal);
        winTotal.text = ("Candies Collected: " + currentTotal + " / " + WinTotal);
        loseTotal.text = ("Candies Collected: " + currentTotal + " / " + WinTotal);
    }

    public void UnpauseGame()
    {
        GamePaused = false;
        PlayerHUD.SetActive(true);
        PausePanel.SetActive(false);

    }

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Carrot")
        {
            currentHealth -= 1;
        }//tracks if player hits a carrot and initiates lose condition

        if(other.gameObject.tag == "Candy")
        {
            currentTotal += 1;
        }//adds 1 to current collable total based on the object's tag

       if(other.gameObject.tag == "TeleCandy")
        {
            if (Sender1.GetComponent<TeleportScript>().Sending == true && Reciever1.GetComponent<TeleportRecieveScript>().recieving == true)
            {
                target = Sender1.GetComponent<TeleportScript>().TeleportTo;
            }//fetching coordinates and detecting bools for teleporter set 1
            
        }
       if(other.gameObject.tag == "TeleReciever")
        {
            if(Reciever1.GetComponent<TeleportRecieveScript>().sending == true && Sender1.GetComponent<TeleportScript>().Recieving == true)
            {
                target = Reciever1.GetComponent<TeleportRecieveScript>().SendTo;
            }//fetching coordinates and detecting bools for teleporter set 1
            if (other.gameObject.tag == "TS2")
            {
                if (Sender2.GetComponent<TeleportScript>().Sending == true && Reciever2.GetComponent<TeleportRecieveScript>().recieving == true)
                {
                    target = Sender2.GetComponent<TeleportScript>().TeleportTo;
                }//fetching coordinates and detecting bools for teleporter set 1
            }

            if (other.gameObject.tag == "TS3")
            {
                if (Sender3.GetComponent<TeleportScript>().Sending == true && Reciever3.GetComponent<TeleportRecieveScript>().recieving == true)
                {
                    target = Sender3.GetComponent<TeleportScript>().TeleportTo;
                }//fetching coordinates and detecting bools for teleporter set 1
            }

            if (other.gameObject.tag == "TR2")
            {
                if (Reciever2.GetComponent<TeleportRecieveScript>().sending == true && Sender2.GetComponent<TeleportScript>().Recieving == true)
                {
                    target = Reciever2.GetComponent<TeleportRecieveScript>().SendTo;
                }//fetching coordinates and detecting bools for teleporter set 1
            }

            if (other.gameObject.tag == "TR3")
            {
                if (Reciever3.GetComponent<TeleportRecieveScript>().sending == true && Sender3.GetComponent<TeleportScript>().Recieving == true)
                {
                    target = Reciever3.GetComponent<TeleportRecieveScript>().SendTo;
                }//fetching coordinates and detecting bools for teleporter set 1
            }

        }
    }
}
