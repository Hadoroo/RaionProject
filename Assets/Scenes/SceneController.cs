using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public string scene;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        SceneManager.LoadScene(scene);
        DontDestroyOnLoad(colliderObject);
        Destroy(player);

    }
}