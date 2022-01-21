using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIController : MonoBehaviour
{



    public foundVideosUi foundVideosUi;
    public GameObject Button;
    public GameObject Bar;
    public GameObject timeControllerPrefab;
    private GameObject timeControllerObject;

    public CountdownTimer countdownControl;

    TimeController timeControllerScript;

    private Vector3 startPosition;
    private float width;
    private float oldAmount;

    private GameManager myGameManager;

    public bool mainVideoFound = false;

    public VideoVariables mainVideo;

    private bool firstKeyVideoFound = false;
    private bool secondKeyVideoFound = false;
    private bool thirdKeyVideoFound = false;

    void Start()
    {
        timeControllerObject = timeControllerPrefab.transform.GetChild(0).GetChild(0).gameObject;
        myGameManager = FindObjectOfType<GameManager>();
        timeControllerScript = timeControllerObject.GetComponent<TimeController>();
        width = ((RectTransform)Bar.transform).rect.width;
        startPosition = Button.transform.localPosition;
        oldAmount = 0f;
    }

    void Update()
    {
        checkWin();
        findMainVideo();

        if (mainVideoFound)
        {
            if (mainVideo.IsRelevant && mainVideo.CurrentTime >= mainVideo.StartTimeHighlight && mainVideo.CurrentTime <= mainVideo.EndTimeHighlight)
            {
                if (firstKeyVideoFound)
                {
                    if (secondKeyVideoFound)
                    {
                        if(mainVideo.Type == "key3")
                        {
                            foundVideosUi.SetClueActive(3);
                            addFoundMarkToVideoAndPlaySound("key3");
                            mainVideo.HasBeenFound = true;
                            thirdKeyVideoFound = true;
                        }
                        else
                        {
                            checkForFirstFillersFound();
                            checkForSecondFillersFound();
                        }
                    }
                    else if(mainVideo.Type == "key2")
                    {
                        foundVideosUi.SetClueActive(2);
                        addFoundMarkToVideoAndPlaySound("key2");
                        mainVideo.HasBeenFound = true;
                        secondKeyVideoFound = true;
                    } else
                    {
                        checkForFirstFillersFound();
                    }
                } 
                else if(mainVideo.Type == "key1")
                    {
                        foundVideosUi.SetClueActive(1);
                        addFoundMarkToVideoAndPlaySound("key1");
                        mainVideo.HasBeenFound = true;
                        firstKeyVideoFound = true;
                    }
                
            }
                if (!timeControllerScript.isColliding)
                {
                    Button.GetComponent<SpriteRenderer>().size = new Vector2(100, 100);
                    foundVideosUi.ResetBorder();
                    //rückgänging
                    Bar.GetComponent<SpriteRenderer>().color = Color.gray;
                    var percentage = (float)mainVideo.CurrentPercentage;
                    if(percentage < 0)
                {
                    percentage = 0f;
                }
                    timeControllerObject.transform.position = new Vector3(percentage * 140,
                    timeControllerObject.transform.position.y, timeControllerObject.transform.position.z);
                    moveCursor((float)percentage, false);
                }
                else if (timeControllerScript.isColliding)
                {
                    Button.GetComponent<SpriteRenderer>().size = new Vector2(125, 125);
                    Bar.GetComponent<SpriteRenderer>().color = new Color(0.06f, 0.25f, 0.5f, 1f);
                    foundVideosUi.SetBorder();
                    //anzeigen
                    var newPercentage = Mathf.Clamp(timeControllerObject.transform.position.x / 140, 0f, 1.0f);
                    moveCursor((float)newPercentage, true);
            }
        }
    }

    void checkForFirstFillersFound()
    {
        if (mainVideo.Type == "filler1")
        {
            addFoundMarkToVideoAndPlaySound("filler1");
            mainVideo.HasBeenFound = true;
        }
        else if (mainVideo.Type == "filler2")
        {
            addFoundMarkToVideoAndPlaySound("filler2");
            mainVideo.HasBeenFound = true;
        }
        else if (mainVideo.Type == "filler3")
        {
            addFoundMarkToVideoAndPlaySound("filler3");
            mainVideo.HasBeenFound = true;
        }
    }

    void checkForSecondFillersFound()
    {
        if (mainVideo.Type == "filler4")
        {
            addFoundMarkToVideoAndPlaySound("filler4");
            mainVideo.HasBeenFound = true;
        } 
        else if (mainVideo.Type == "filler5")
        {
            addFoundMarkToVideoAndPlaySound("filler5");
            mainVideo.HasBeenFound = true;
        }
    }


    bool reseted = false;

    void moveCursor(float currentAmount, bool setNewValue)
    {
        if(!myGameManager.moving && !reseted){
            oldAmount = currentAmount;  
            reseted = true;  
        }
        if(myGameManager.moving){
            reseted = false;
        }
        
        if(currentAmount <= 0)
        {
            Button.transform.localPosition = startPosition + new Vector3(width * 0.01f, 0, 0);
            Button.transform.localPosition = new Vector3(Mathf.Clamp(Button.transform.localPosition.x, startPosition.x, startPosition.x + width),
                Mathf.Clamp(Button.transform.localPosition.y, startPosition.y, startPosition.y),
                Mathf.Clamp(Button.transform.position.z, startPosition.z, startPosition.z));
            oldAmount = 0.01f;
            mainVideo.CurrentPercentage = 0.01f;
        }
        if (Mathf.Abs(oldAmount - currentAmount) < 0.15f)
        {
            Button.transform.localPosition = startPosition + new Vector3(width * currentAmount, 0, 0);
            Button.transform.localPosition = new Vector3(Mathf.Clamp(Button.transform.localPosition.x, startPosition.x, startPosition.x + width),
                Mathf.Clamp(Button.transform.localPosition.y, startPosition.y, startPosition.y),
                Mathf.Clamp(Button.transform.position.z, startPosition.z, startPosition.z));
            oldAmount = currentAmount;
            if (setNewValue)
            {
            mainVideo.CurrentPercentage = currentAmount;
            }
        }
    }

    void addFoundMarkToVideoAndPlaySound(string videoType)
    {
        foreach (listElement element in myGameManager.allListElements)
        {
            if (element.isMainVideo)
            {
                if (!element.hasBeenFound)
                {
                    switch (videoType)
                    {
                        case "filler1":
                            SoundManager.PlaySound(SoundManager.Sound.FillerVoiceline1);
                            break;
                        case "filler2":
                            SoundManager.PlaySound(SoundManager.Sound.FillerVoiceline2);
                            break;
                        case "filler3":
                            SoundManager.PlaySound(SoundManager.Sound.FillerVoiceline3);
                            break;
                        case "filler4":
                            SoundManager.PlaySound(SoundManager.Sound.FillerVoiceline4);
                            break;
                        case "filler5":
                            SoundManager.PlaySound(SoundManager.Sound.FillerVoiceline5);
                            break;
                        case "key1":
                            SoundManager.PlaySound(SoundManager.Sound.KeyVoiceline1);
                            break;
                        case "key2":
                            SoundManager.PlaySound(SoundManager.Sound.KeyVoiceline2);
                            break;
                        case "key3":
                            SoundManager.PlaySound(SoundManager.Sound.KeyVoiceline3);
                            break;
                    }
                    
                }
                element.hasBeenFound = true;
            }
        }
    }

    void findMainVideo()
    {
        foreach (listElement element in myGameManager.allListElements)
        {
            if (element.isMainVideo)
            {
                mainVideo = element.GetComponent<VideoVariables>();
                this.mainVideoFound = true;
            }  
        }

    }

    void checkWin()
    {
        if(firstKeyVideoFound & secondKeyVideoFound & thirdKeyVideoFound)
        {
            countdownControl.freezeTimer = true;
            StartCoroutine(SwitchSceneAfterTime(14));
        }
    }

    IEnumerator SwitchSceneAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("WinScene");
    }
}
