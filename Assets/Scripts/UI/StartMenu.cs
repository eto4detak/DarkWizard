using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public Text titleText;
    public Button levelSelectConfirmButton;


    public void StartLevel()
    {
        LevelManager.instance.StartLevel();
    }


}
