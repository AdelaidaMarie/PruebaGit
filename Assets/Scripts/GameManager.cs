using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool gamePaused;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UpdateGamePause();
        }
    }
    private void UpdateGamePause()
    {
        gamePaused = !gamePaused;
       
        
        Time.timeScale = (gamePaused) ? 0.0f : 1f;

        Cursor.lockState = (gamePaused) ? CursorLockMode.None : CursorLockMode.Locked;


    }
}
