using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOAD : MonoBehaviour
{
    public void PLAY_GAME() {
        SceneManager.LoadSceneAsync(1);
    }
    // Start is called before the first frame update
    public void QUIT_GAME()
    {
        Application.Quit();
    }
}
