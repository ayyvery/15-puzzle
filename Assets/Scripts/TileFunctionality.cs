using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileFunctionality : MonoBehaviour
{

    public List<string> coords = new List<string>() { "-413,583", "-137,583", "137,583", "413,583", "-413,289", "-137,289", "137,289", "413,289", "-413,-2", "-137,-2", "137,-2", "413,-2", "-413,-293", "-137,-293", "137,-293", "413,-293" };
    public List<string> coordsAlt = new List<string>() { "-413,583", "-137,583", "137,583", "413,583", "-413,289", "-137,289", "137,289", "413,289", "-413,-2", "-137,-2", "137,-2", "413,-2", "-413,-293", "-137,-293", "137,-293", "413,-293" };
    
    public string[] tiles = new string[15];
    public string blank;

    public GameObject[] tileObjectList;
    public Text gameTurnCounter;
    public Text gameTimeMeter;

    public Text winDialogueTurnCounter;
    public Text winDialogueTimeMeter;

    public Text winDialogueRecordTurnCounter;
    public Text winDialogueRecordTimeMeter;

    public Text recordWinDialogueTurnCounter;
    public Text recordWinDialogueTimeMeter;

    public int turnCounter = 0;

    public int moveTracker = 6;
    public int tileNumber = 0;

    public int tileX;
    public int tileY;

    public int blankX;
    public int blankY;

    public AudioClip slideSound;
    public AudioClip winSound;
    public AudioClip clickSound;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    string[] separatorDot = { "." };

    public bool countTime = true;

    public string finalTime;
    public string finalTurns;

    public bool isNewRecord = false;

    public GameObject winMenu;
    public GameObject recordWinMenu;

    public bool hasWon = false;

    public double startSeconds;

    void Start()
    {
        tileObjectList = new GameObject[] { GameObject.Find("1"), GameObject.Find("2"), GameObject.Find("3"), GameObject.Find("4"), GameObject.Find("5"), GameObject.Find("6"), GameObject.Find("7"), GameObject.Find("8"), GameObject.Find("9"), GameObject.Find("10"), GameObject.Find("11"), GameObject.Find("12"), GameObject.Find("13"), GameObject.Find("14"), GameObject.Find("15") };

        for (int i = 0; i < 15; i++)
        {
            int rand = UnityEngine.Random.Range(0, coords.Count);
            tiles[i] = coords[rand];
            coords.Remove(coords[rand]);
            string[] separator = { "," };
            tileObjectList[i].transform.position = new Vector3(int.Parse(tiles[i].Split(separator, 2, StringSplitOptions.RemoveEmptyEntries)[0]), int.Parse(tiles[i].Split(separator, 2, StringSplitOptions.RemoveEmptyEntries)[1]), -2);
        }

        blank = coords[0];

        gameObject.AddComponent<AudioSource>();
        source.clip = slideSound;
        source.clip = winSound;
        source.clip = clickSound;
        source.playOnAwake = false;
        /*
        winMenu = GameObject.FindWithTag("WinMenu");
        recordWinMenu = GameObject.FindWithTag("RecordWinMenu");
        */
        gameTurnCounter.text = turnCounter.ToString();

        startSeconds = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
    }

    public void OnTileClick(int tilenumber)
    {
        tileNumber = tilenumber;
        string middleman = tiles[tilenumber];
        string[] separator = { "," };
        string[] separatorDoubleDot = { ":" };

        tileX = int.Parse(tiles[tilenumber].Split(separator, StringSplitOptions.RemoveEmptyEntries)[0]);
        tileY = int.Parse(tiles[tilenumber].Split(separator, StringSplitOptions.RemoveEmptyEntries)[1]);

        blankX = int.Parse(blank.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0]);
        blankY = int.Parse(blank.Split(separator, StringSplitOptions.RemoveEmptyEntries)[1]);

        int tilePos = coordsAlt.IndexOf(tiles[tilenumber]);

        if (tilePos + 4 < coordsAlt.Count && tilePos - 4 >= 0)
        {
            if (blank == coordsAlt[tilePos + 4] || blank == coordsAlt[tilePos - 4] || blank == coordsAlt[tilePos + 1] || blank == coordsAlt[tilePos - 1])
            {
                tiles[tilenumber] = blank;
                blank = middleman;
                turnCounter++;

                gameTurnCounter.text = turnCounter.ToString();

                moveTracker = 0;
            }
        } else if ((tilePos >= 12 && tilePos <= 14) && (blank == coordsAlt[tilePos - 4] || blank == coordsAlt[tilePos + 1] || blank == coordsAlt[tilePos - 1]))
        {
            tiles[tilenumber] = blank;
            blank = middleman;
            turnCounter++;

            gameTurnCounter.text = turnCounter.ToString();

            moveTracker = 0;

        } else if ((tilePos == 15) && (blank == coordsAlt[tilePos - 4] || blank == coordsAlt[tilePos - 1]))
        {
            tiles[tilenumber] = blank;
            blank = middleman;
            turnCounter++;

            gameTurnCounter.text = turnCounter.ToString();

            moveTracker = 0;

        } else if ((tilePos >= 1 && tilePos <= 3) && (blank == coordsAlt[tilePos + 4] || blank == coordsAlt[tilePos + 1] || blank == coordsAlt[tilePos - 1]))
        {
            tiles[tilenumber] = blank;
            blank = middleman;
            turnCounter++;

            gameTurnCounter.text = turnCounter.ToString();

            moveTracker = 0;

        } else if ((tilePos == 0) && (blank == coordsAlt[tilePos + 4] || blank == coordsAlt[tilePos + 1]))
        {
            tiles[tilenumber] = blank;
            blank = middleman;
            turnCounter++;

            gameTurnCounter.text = turnCounter.ToString();

            moveTracker = 0;
        }

        //after completion
        if (tiles[0] == coordsAlt[0] && tiles[1] == coordsAlt[1] && tiles[2] == coordsAlt[2] && tiles[3] == coordsAlt[3] && tiles[4] == coordsAlt[4] && tiles[5] == coordsAlt[5] && tiles[6] == coordsAlt[6] && tiles[7] == coordsAlt[7] && tiles[8] == coordsAlt[8] && tiles[9] == coordsAlt[9] && tiles[10] == coordsAlt[10] && tiles[11] == coordsAlt[11] && tiles[12] == coordsAlt[12] && tiles[13] == coordsAlt[13] && tiles[14] == coordsAlt[14])
        {
            hasWon = true;
        }

        if (hasWon)
        {
            countTime = false;
            finalTurns = turnCounter.ToString();
            finalTime = gameTimeMeter.text;

            int lowestMinutes = int.Parse(PlayerPrefs.GetString("lowestTime", "999999:99").Split(separatorDoubleDot, StringSplitOptions.RemoveEmptyEntries)[0]);
            int lowestSeconds = int.Parse(PlayerPrefs.GetString("lowestTime", "999999:99").Split(separatorDoubleDot, StringSplitOptions.RemoveEmptyEntries)[1]);

            int finalMinutes = int.Parse(finalTime.Split(separatorDoubleDot, StringSplitOptions.RemoveEmptyEntries)[0]);
            int finalSeconds = int.Parse(finalTime.Split(separatorDoubleDot, StringSplitOptions.RemoveEmptyEntries)[1]);

            if (PlayerPrefs.GetInt("lowestTurns", 9999999) > int.Parse(finalTurns))
            {
                PlayerPrefs.SetInt("lowestTurns", int.Parse(finalTurns));
                isNewRecord = true;
            }

            if (lowestMinutes > finalMinutes || (lowestMinutes == finalMinutes && lowestSeconds > finalSeconds))
            {
                PlayerPrefs.SetString("lowestTime", finalTime);
                isNewRecord = true;
            }

            if (isNewRecord)
            {
                recordWinMenu.SetActive(true);
                recordWinDialogueTurnCounter.text = finalTurns;
                recordWinDialogueTimeMeter.text = finalTime;
            }
            else
            {
                winMenu.SetActive(true);
                winDialogueRecordTurnCounter.text = PlayerPrefs.GetInt("lowestTurns", 0).ToString();
                winDialogueRecordTimeMeter.text = PlayerPrefs.GetString("lowestTime", "err");
                winDialogueTurnCounter.text = finalTurns;
                winDialogueTimeMeter.text = finalTime;
            }

            source.PlayOneShot(winSound);
        }
    }

    //win dialogue functionality
    public void onPlayAgainButtonClick()
    {
        source.PlayOneShot(clickSound);
        SceneManager.LoadScene("Game");
    }
    public void onQuitButtonClick()
    {
        
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (moveTracker < 6)
        {
            tileObjectList[tileNumber].transform.position = Vector3.Lerp(new Vector3(tileX, tileY, -2), new Vector3(blankX, blankY, -2), 0.2f * moveTracker);
            moveTracker++;
            
        }

        if (moveTracker == 5)
        {
            source.PlayOneShot(slideSound);
        }

        //int time = int.Parse(Time.timeSinceLevelLoad.ToString().Split(separatorDot, StringSplitOptions.RemoveEmptyEntries)[0]);
        int time = Convert.ToInt32(new TimeSpan(DateTime.Now.Ticks).TotalSeconds - startSeconds);

        if (countTime)
        {
            if (time < 10)
            {
                gameTimeMeter.text = "0:0" + time.ToString();
            } 
            else if (time >= 10 && time < 60)
            {
                gameTimeMeter.text = "0:" + time.ToString();
            }
            else if (time % 60 < 10 && time >= 60)
            {
                gameTimeMeter.text = Math.Floor(time / 60.0).ToString() + ":" + "0" + (time % 60).ToString();
            } 
            else if (time % 60 >= 10 && time >= 60)
            {
                gameTimeMeter.text = Math.Floor(time / 60.0).ToString() + ":" + (time % 60).ToString();
            }
        }
    }

}

