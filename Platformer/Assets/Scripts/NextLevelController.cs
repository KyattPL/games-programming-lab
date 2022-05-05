using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj) {
        if (obj.tag == "Player") {
            int currScene = PlayerPrefs.GetInt("curr_level", 1);
            PlayerPrefs.SetInt("curr_level", currScene + 1);
            PlayerPrefs.SetInt("player_score", player.score);
            PlayerPrefs.SetInt("player_lives", player.lives);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Level-" + (currScene + 1));
        }
    }
}