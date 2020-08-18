using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctionality : MonoBehaviour
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


    public void OnGiveUpButtonClick()
    {
        source.PlayOneShot(clickSound);
    }
}
