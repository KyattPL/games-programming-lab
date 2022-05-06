using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StarterAssets;

public class HUDController : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject livesText;
    public FirstPersonController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController.OnScoreUpdate += UpdateScore;
        playerController.OnLivesUpdate += UpdateLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore()
    {
        scoreText.GetComponent<Text>().text = $"Score: {playerController.score}";
    }

    void UpdateLives()
    {
        livesText.GetComponent<Text>().text = $"Lives: {playerController.lives}";
    }
}
