using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

using UDebug = UnityEngine.Debug;  //Alvins Debug
using SDebug = System.Diagnostics.Debug;  //Systems Debug
//Unity was having issues with "Debug" being too ambiguous,it was being called from two diffrent packages


// track how many lives the player has
// respawn if they fall in water with lives > 0
//allow the player to replay/quit
// End the game if the player dies

public class GameManager : MonoBehaviour
{
    public int Lives = 3;
    public Vector3 RespawnPoint;
    public GameObject Player;
    public GameObject endScene;

    private bool isGamedone = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamedone) {
            return; 
        
        }
    }
    public void InWater()
    {
        if (Lives > 0)
        {
            Respawn();
            UDebug.Log("Player has Respawned with" + Lives);
        }
      
        else
        {
            EndGame();
            UDebug.Log("Player has died with");
        }
        ;
    }

    private void Respawn()
    {
        Lives--;   //decreases the life after every "death"
        Player.transform.position  = RespawnPoint;
    }
    //Method to call the ENDSCENE when a Player dies
    private void EndGame()
    {
        isGamedone = true;
        SceneManager.LoadScene("End Scene");
    }
}
