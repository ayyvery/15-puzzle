using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source { get { return GetComponent<AudioSource>();  } }

    public Text recordTurnCounter;
    public Text recordTimeMeter;

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = clickSound;
        source.playOnAwake = false;

        recordTurnCounter.text = PlayerPrefs.GetInt("lowestTurns", 0).ToString();
        recordTimeMeter.text = PlayerPrefs.GetString("lowestTime", "0:00");
    }

    public void OnPlayButtonClick()
    {
        source.PlayOneShot(clickSound);
        SceneManager.LoadScene("Game");
    }
}
