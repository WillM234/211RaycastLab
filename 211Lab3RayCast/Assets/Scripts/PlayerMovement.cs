using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 target;
    public Vector2 startPos;

    private bool GamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        target = startPos;
        
        //dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;

        bool notMoving = ((Vector2)transform.position == target);
        if(Input.GetKeyDown(KeyCode.P))
        {
            GamePaused = true;
        }
        if(GamePaused == false)
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
        }
        

        if(Input.GetKeyDown(KeyCode.R))//reloads scene
        {
            EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
        }

        //Raycasting
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        Debug.DrawLine(transform.position, transform.position + (Vector3)direction, Color.yellow);
        if(hit.collider != null)
        {
            if(hit.collider.tag == "")
            {
                target = hit.transform.position;
            }
            //else if(hit.collider.tag == "")
                }
    }
    public void UnpasueGame()
    {
        GamePaused = false;
    }

}
