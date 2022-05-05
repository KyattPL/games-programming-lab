using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (PlayerPrefs.HasKey("curr_level") && activeScene.name != "Level-" + PlayerPrefs.GetInt("curr_level")) {
            SceneManager.LoadScene("Level-" + PlayerPrefs.GetInt("curr_level"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
