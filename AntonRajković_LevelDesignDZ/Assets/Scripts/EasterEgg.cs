using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class EasterEgg : MonoBehaviour
{
    public bool ChanceToSee;
    public RawImage EasterEggVideo;
    public RawImage VictoryVideo;
    public TextMeshProUGUI Text;
    public VideoPlayer EasterEggvideoPlayer;
    public VideoPlayer VictoryVideoPlayer;
    public AudioSource MyAudioSource;
    public AudioClip VictoryMusic;


    // Start is called before the first frame update
    void Start()
    {
        ChanceToSee = (Random.value > 0.9f);
        if (ChanceToSee)
        {
            EasterEggvideoPlayer.enabled = true;
            EasterEggVideo.enabled = true;
            VictoryVideoPlayer.enabled = false;
            VictoryVideo.enabled = false;
            Text.text = "The zombies are gone!";
            MyAudioSource.clip = null;
            MyAudioSource.Play();
        }
        else
        {
            EasterEggvideoPlayer.enabled = false;
            EasterEggVideo.enabled = false;
            VictoryVideoPlayer.enabled = true;
            VictoryVideo.enabled = true;
            Text.text = "You won! You killed all the zombies!";
            MyAudioSource.clip = VictoryMusic;
            MyAudioSource.Play();
        }
    }

   
}
