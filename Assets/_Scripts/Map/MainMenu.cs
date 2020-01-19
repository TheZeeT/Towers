using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Camera Cam;
    public Light Sun;
    
    
    void Update()
    {

        //Rotating camera and sun around zero-zero
        Cam.gameObject.transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
        Sun.gameObject.transform.Rotate(Vector3.left, 20f * Time.deltaTime);
        

    }

    public void PlayTheGame()
    {
        //play button
        SceneManager.LoadScene("LevelSelector");
    }
}
