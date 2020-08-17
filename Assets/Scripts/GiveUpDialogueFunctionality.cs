using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GiveUpDialogueFunctionality : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = clickSound;
        source.playOnAwake = false;
    }

    public void OnQuitButtonClick()
    {
        source.PlayOneShot(clickSound);
        SceneManager.LoadScene("Menu");
    }

    public void OnReturnButtonClick()
    {
        source.PlayOneShot(clickSound);
    }
}
