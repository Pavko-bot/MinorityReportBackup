using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public bool kinectMode = false;
    private VideoController myVideoController;
    public listElement mainVideo;

    public int mainVideoRow = 2;

    [HideInInspector]
    public bool oneRowIsActive = true;
    [HideInInspector]
    public listElement[] allListElements;

    private float xPosStart = 0;

    public bool moving = false;


    public float multiplier = 10f;
    private int currentVideoRow = 2;

    public static GameManager instance;

    private void Awake()
    {
        SoundManager.Initialize();

        instance = this;
    }

    // Start is called before the first frame update

    void Start()
    {
        myVideoController = FindObjectOfType<VideoController>();
        this.allListElements = FindObjectsOfType<listElement>();
    }

    // Update is called once per frame
    void Update()
    {
        findMainVideo();
        checkIfMoving();
    }

    void checkIfMoving()
    {
        foreach (listElement element in this.allListElements)
        {
            if (element.isMoving)
            {
                this.moving = true;
                break;
            }
            this.moving = false;
        }
    }

    void findMainVideo()
    {
        if (this.mainVideo == null)
        {
            foreach (listElement element in allListElements)
            {
                if (element.isMainVideo)
                    this.mainVideo = element;
            }
        }

    }

    private void setActiveRow(int rowIndex)
    {
        foreach (listElement element in this.allListElements)
        {
            if (element.row == rowIndex)
            {
                if (!element.isActiveRow)
                    element.setActiveRow(true);
            }
            else
            {
                element.setActiveRow(false);
            }
        }

        myVideoController.rePositionVideos(rowIndex);
    }
    public void MoveRowUp()
    {
        switch (mainVideoRow)
        {
            case 1:
                setActiveRow(2);
                mainVideoRow = 2;
                break;
            case 2:
                setActiveRow(3);
                mainVideoRow = 3;
                break;
            case 3:
                setActiveRow(1);
                mainVideoRow = 1;
                break;
            default:
                break;
        }
        Debug.Log("main row" + mainVideoRow);
    }
    public void MoveRowDown()
    {
        switch (mainVideoRow)
        {
            case 1:
                setActiveRow(3);
                mainVideoRow = 3;
                break;
            case 2:
                setActiveRow(1);
                mainVideoRow = 1;
                break;
            case 3:
                setActiveRow(2);
                mainVideoRow = 2;
                break;
            default:
                break;
        }
        Debug.Log("main row" + mainVideoRow);
    }

    public void MoveRowLeft()
    {
        Debug.Log("MOVE ROW LEFT");
        moveRow("left");
    }

    public void MoveRowRight()
    {
        Debug.Log("MOVE ROW RIIGHT");
        moveRow("right");
        
    }

    private void moveRow(string direction)
    {
        foreach (listElement element in this.allListElements)
        {
            if (element.isActiveRow && element.row == this.mainVideoRow)
            {
                this.move(direction, element);
                myVideoController.rePositionVideos(element.row);
            }
            else if (element.isActiveRow && element.row != this.mainVideoRow)
            {
                if (direction == "right")
                {
                    element.moveRight();
                    myVideoController.rePositionVideos(element.row);
                }
                if (direction == "left")
                {

                    if (element.posIndex == myVideoController.mainVideoIndex)
                    {
                        this.pushVideoToLeft(this.mainVideoRow);
                        element.moveSpecial();
                        myVideoController.rePositionVideos(element.row);
                        return;
                    }
                }
            }
        }

    }

    private void pushVideoToLeft(int rowToGoLeft)
    {
        foreach (listElement element in this.allListElements)
        {
            if (element.row == rowToGoLeft)
            {
                this.move("left", element);
            }

        }
    }

    private void move(string direction, listElement element)
    {
        if (direction == "right")
            element.moveRight();
        else if (direction == "left")
            element.moveLeft();


    }
}
