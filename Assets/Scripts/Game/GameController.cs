using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
     public void ReplayGame(){
        SceneManager.LoadScene("OutdoorsScene");
     }

     public void QuitGame(){
        Application.Quit();
     }
}
