using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageInput : MonoBehaviour
{
    //public MessageParser messageParser;
    //public GameManager gameManager;
    //
    //public GameObject handTracker;
    //private List<float> copyInputBuffer;
    //
    //// Start is called before the first frame update
    //void Start()
    //{
    //    messageParser = FindObjectOfType<MessageParser>();
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    getMousePosition();
    //    getArrowKeyInput();
    //
    //    if (gameManager.kinectMode)
    //    {
    //        getKinectPosition();
    //        getKinectGestures();
    //    }
    //}
    //
    //private void getKinectPosition()
    //{
    //    if (gameManager.moving)
    //        return;
    //    //Vector3 handPos = messageParser.GetHandPosition();
    //    //handPos = -handPos;
    //    //handTracker.transform.position = new Vector2(handTracker.transform.position.x, handPos.y);
    //    ////Debug.Log(handPos.y);
    //    ////if (handPos.y >= -250 && handPos.y <= 0)
    //    ////{
    //    ////    gameManager.setActiveRow(1);
    //    ////}
    //    //
    //    //if (handPos.y > 0 && handPos.y <= 250)
    //    //{
    //    //    //gameManager.setActiveRow(2);
    //    //}
    //    //
    //    //if (handPos.y > 250 && handPos.y <= 500)
    //    //{
    //    //    gameManager.setActiveRow(3);
    //    //}
    //
    //}
    //
    //private int counter = 0;
    //
    //
    //
    //void getKinectGestures()
    //{
    //    if (messageParser.GetLeftSwipeGesture())
    //    {
    //        gameManager.moveRow("left");
    //    } else if (messageParser.GetRightSwipeGesture()){
    //        gameManager.moveRow("right");
    //    }
    //}
    //
    //void getMousePosition()
    //{
    //    if (gameManager.moving)
    //        return;
    //
    //    if(Input.mousePosition.y >= 0 && Input.mousePosition.y <= 1080 / 3)
    //    {
    //        gameManager.setActiveRow(1);
    //    }
    //
    //    if (Input.mousePosition.y > 1080 / 3 && Input.mousePosition.y <= (1080 / 3) * 2)
    //    {
    //        gameManager.setActiveRow(2);
    //    }
    //
    //    if (Input.mousePosition.y > ((1080 / 3) * 2) && Input.mousePosition.y <= 1080)
    //    {
    //        gameManager.setActiveRow(3);
    //    }
    //
    //}
    //
    //void getArrowKeyInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        gameManager.moveRow("right");
    //    }
    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        gameManager.moveRow("left");
    //    }
    //}






}
