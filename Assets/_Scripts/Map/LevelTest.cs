using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTest : MonoBehaviour
{
 
    public void Load1()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
    }

    public void ExitLevel()
    {
        SceneManager.UnloadSceneAsync("TestScene");
    }

}
