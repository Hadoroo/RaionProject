using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public string scene;

    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        SceneManager.LoadScene(scene);
    }

    public void play()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void restart()
    {
        SceneManager.LoadScene("MainMenu");
    }



        public void Quit()
        {
            Application.Quit();
        }
    }