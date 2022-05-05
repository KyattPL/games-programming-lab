using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Toggle musicToggle;
    [SerializeField]
    private Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        player.OnGemPickup += UpdateScore;
        player.OnEnemyHit += UpdateHealth;

        scoreText.text = "Score: " + PlayerPrefs.GetInt("player_score", 0);
        livesText.text = "Lives: " + PlayerPrefs.GetInt("player_lives", 3);

        int musicState = PlayerPrefs.GetInt("music_state", 1);
        
        int count = musicToggle.onValueChanged.GetPersistentEventCount();
        for (int i=0; i < count; i++) {
            musicToggle.onValueChanged.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
        }

        musicToggle.isOn = musicState == 1 ? true : false;

        for (int i=0; i < count; i++) {
            musicToggle.onValueChanged.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        }

        musicSlider.value = PlayerPrefs.GetFloat("music_volume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore() {
        scoreText.text = "Score: " + player.score;
    }

    void UpdateHealth() {
        livesText.text = "Lives: " + player.lives;
    }

    public void HandleGameReset() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level-1");
    }
}
