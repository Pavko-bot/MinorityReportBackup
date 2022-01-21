using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class foundVideosUi : MonoBehaviour
{
    public Sprite filledSprite;
    public GameObject clue1, clue2 , clue3;
    private Image border;

    void Start(){
        border = GetComponent<Image>();
    }
    

    public void SetClueActive(int number){
        switch(number){
            case 1:
                changeOnFilled(clue1);
                clue1.SetActive(true);
                break;
            case 2:
                changeOnFilled(clue2);
                clue2.SetActive(true);
                break;
            case 3:
                changeOnFilled(clue3);
                clue3.SetActive(true);
                break;
        }
    }



    private void changeOnFilled(GameObject clue)
    {
        Image tmp = clue.transform.parent.GetComponent<Image>();
        tmp.sprite = filledSprite;
        tmp.color = new Color(129,129,129,255);
    }

    public void ResetBorder(){
        border.color = new Color(255,0,0,255);
    }

    public void SetBorder(){
        border.color = new Color(0,255,255,255);
    }


}
