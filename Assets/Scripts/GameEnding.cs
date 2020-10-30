using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// for game restart

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 2f;
    private float Timer;

    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    private bool hasAudioPlayed;// make sure audio only plays once

    private bool isPlayerAtExit;
    private bool isPlayerCaught;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed) // audio will only play once
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        Timer += Time.deltaTime; // deltaTime is time between frames
        imageCanvasGroup.alpha = Timer / fadeDuration; // alpha is ranged from 0-1;

        if (Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0); // load first scene of the game
            }
            else
            { 
                Application.Quit(); // only works after build
            }
        }
    }

    // other scripts can globally call this method
    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }
}
