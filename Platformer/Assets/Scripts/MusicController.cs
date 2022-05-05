using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int read_state = PlayerPrefs.GetInt("music_state", 1);
        if (read_state == 0) {
            GetComponent<AudioSource>().Pause();
        } else {
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMusicState() {
        AudioSource musicPlayer = GetComponent<AudioSource>();
        if (musicPlayer.isPlaying) {
            musicPlayer.Pause();
        } else {
            musicPlayer.Play();
        }
        //Debug.Log(musicPlayer.isPlaying ? 1 : 0);
        PlayerPrefs.SetInt("music_state", musicPlayer.isPlaying ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(Slider sliderObj) {
        GetComponent<AudioSource>().volume = sliderObj.value;
        PlayerPrefs.SetFloat("music_volume", sliderObj.value);
        PlayerPrefs.Save();
    }
}
