using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class listElement : MonoBehaviour
{
    public Image border;

    public listElement next,previous;

    public Vector2 pos,targetPos;

    public Vector2 currentScale,targetScale;

    public float stepSize = 20f;

    public int row,index,maxCountInRow;

    public int posIndex;

    public bool linked = false;

    private listElement[] allListElements;

    public float stepXSizeScale, stepYSizeScale;

    public bool isActiveRow,isMainVideo,hasBeenFound = false;

    public Vector2 activeVideo;
    public Vector2 passivVideo;

    private VideoController myVideoController;
    public GameManager myGameManager;
    private Vector2[] positions;

    public float distance = 0;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
  
        activeVideo = transform.localScale * 1.5f;
        passivVideo = transform.localScale;

        myVideoController = FindObjectOfType<VideoController>();
        myGameManager = FindObjectOfType<GameManager>();

        this.setPositionsToTrack();

        this.oldPosIndex = this.posIndex;
        this.newPosIndex = this.posIndex;
    }

    void setPositionsToTrack()
    {
        switch (row)
        {
            case 1:
                this.positions = myVideoController.positions_1;
                break;
            case 2:
                this.positions = myVideoController.positions_2;
                break;
            case 3:
                this.positions = myVideoController.positions_3;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.distance = Vector2.Distance(this.pos, this.targetPos);
        this.linking();
        this.moving();

        //this.snapSizing();
        
    }





    public void moving()
    {

        if (myVideoController != null)
            this.stepSize = (Vector2.Distance(this.pos, this.targetPos) / myVideoController.stepTillTarget);

        Vector2 dir = ((Vector2) this.targetPos- (Vector2) this.transform.position).normalized;


        transform.position = (Vector2)transform.position + dir * this.stepSize;
        //transform.position = Vector2.MoveTowards(transform.position, this.targetPos, this.stepSize);
        this.setSizeOfVideo();
    
        if(this.pos == targetPos)
        {
            isMoving = false;
        }else
        {
            isMoving = true;
        }

        if (Vector2.Distance(transform.position, this.targetPos) <= this.stepSize)
        {
            this.pos = this.targetPos;
            this.transform.position = this.pos;
            this.currentScale = this.targetScale;
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = this.currentScale;
        }
    }

    public void moveRight()
    {
        this.posIndex += 1;
   
        if(this.posIndex == (myVideoController.mainVideoIndex+1) && this.isMainVideo)
        {
            this.setAsMainVideo(false);
        }
        if (this.posIndex == myVideoController.mainVideoIndex && !this.isMainVideo)
        {
            this.posIndex = myVideoController.mainVideoIndex;
            this.setOtherMainVideoFalse();
            myGameManager.mainVideoRow = row;
            this.setAsMainVideo(true);
        }
        if (this.posIndex >= this.positions.Length)
        {
            this.posIndex = 0;
        }
        this.setTargetPos(positions[this.posIndex]);
        this.setNextPosIndex();
        SoundManager.PlaySound(SoundManager.Sound.CameraSwitch);
    }

    public void moveLeft()
    {
        this.posIndex -= 1;
        if (this.posIndex == (myVideoController.mainVideoIndex - 1) && this.isMainVideo)
        {
            this.setAsMainVideo(false);
        }
        if (this.posIndex == myVideoController.mainVideoIndex && !this.isMainVideo)
        {
            this.setOtherMainVideoFalse();
            myGameManager.mainVideoRow = row;
            this.setAsMainVideo(true);
        }
        if (this.posIndex < 0)
        {
            this.posIndex = this.positions.Length - 1;
        }
        this.setTargetPos(positions[this.posIndex]);
        this.setNextPosIndex();
        SoundManager.PlaySound(SoundManager.Sound.CameraSwitch);
    }


    private void setNextPosIndex()
    {
        //teleport most right window to the most left
        if (this.targetPos == positions[0])
        {
            this.pos = this.targetPos;
            this.transform.position = this.pos;
            return;
        }
        //teleport most left window to the most right
        if (this.targetPos == positions[positions.Length - 1])
        {
            this.pos = this.targetPos;
            this.transform.position = this.pos;
            return;
        }
    }

    public void moveSpecial()
    {
        this.setOtherMainVideoFalse();
        myGameManager.mainVideoRow = row;
        this.setAsMainVideo(true);
    }

    private void setOtherMainVideoFalse()
    {
        foreach (listElement element in allListElements)
        {
            if (element.isMainVideo)
            {
                element.setAsMainVideo(false);
            }
        }
    }

    public void setActiveRow(bool value)
    {
        this.isActiveRow = value;
    }

    private void setAsMainVideo(bool value)
    {
        this.isMainVideo = value;
    }


    public void setSizeOfVideo()
    {
        RectTransform rectTransform = GetComponent<RectTransform>(); 
        if (myGameManager.oneRowIsActive)
        {
            if (isMainVideo)
            {
                this.targetScale = myVideoController.mainVideoScale;
                this.calcStepSize();
                this.changeSize(rectTransform, myVideoController.mainVideoScale);
                //rectTransform.sizeDelta = new Vector2(myVideoController.mainVideoScale.x, myVideoController.mainVideoScale.y);
            }
            else if (isActiveRow)
            {
                this.targetScale = myVideoController.bigScale;
                this.calcStepSize();
                this.changeSize(rectTransform, myVideoController.bigScale);
                //rectTransform.sizeDelta = new Vector2(myVideoController.bigScale.x, myVideoController.bigScale.y);
            }
            else
            {
                this.targetScale = myVideoController.smallScale;
                this.calcStepSize();
                this.changeSize(rectTransform, myVideoController.smallScale);
                //rectTransform.sizeDelta = new Vector2(myVideoController.smallScale.x, myVideoController.smallScale.y);
            }
        }
        


     }

    private void changeSize(RectTransform rectTransform, Vector2 targetSize)
    {


        if (currentScale == targetScale)
        {
            if (hasBeenFound)
            {
                border.enabled = true;
                RectTransform rt = border.GetComponent(typeof(RectTransform)) as RectTransform;
                rt.sizeDelta = currentScale;
            }
            return;
        }
            

        if (rectTransform.sizeDelta.x < targetSize.x)
            rectTransform.sizeDelta = new Vector2((int)mapDistanceToSizeX(1), rectTransform.sizeDelta.y);
        else if (rectTransform.sizeDelta.x > targetSize.x)
            rectTransform.sizeDelta = new Vector2((int)mapDistanceToSizeX(-1), rectTransform.sizeDelta.y);
        else
            this.currentScale = this.targetScale;

        if (rectTransform.sizeDelta.y < targetSize.y)
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (int)mapDistanceToSizeY(1));
        else if(rectTransform.sizeDelta.y > targetSize.y)
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (int)mapDistanceToSizeY(-1));
        else
            this.currentScale = this.targetScale;
                   
       if(hasBeenFound){
            border.enabled = true;
            RectTransform rt = border.GetComponent (typeof (RectTransform)) as RectTransform;
            rt.sizeDelta = rectTransform.sizeDelta;
        }

    }


    float mapDistanceToSizeX(float lead)
    {
        int allDistance = (int) Vector2.Distance(this.pos, this.targetPos);
        int passedDistance = allDistance - (int)Vector2.Distance(this.transform.position, this.targetPos);
        float ratio = (float)passedDistance / (float)allDistance;

        float allDistanceX = Mathf.Abs(this.targetPos.x - this.pos.x);
        float passedDistanceX = allDistanceX - Mathf.Abs(this.targetPos.x - this.transform.position.x);
        float ratioX =  passedDistanceX / allDistanceX;


        
        float scaleX = (myVideoController.stepTillTarget  * ratioX) * this.stepXSizeScale;
        float returnVal = this.currentScale.x + (lead) * scaleX;

        if(lead == 1)
        {
            if (returnVal >= this.targetScale.x)
                return targetScale.x;
        } else if(lead == -1)
        {
            if (returnVal <= this.targetScale.x)
                return targetScale.x;
        }
        return returnVal;
    }


    float mapDistanceToSizeY(float lead)
    {
        int allDistance = (int)Vector2.Distance(this.pos, this.targetPos);
        int passedDistance = allDistance -(int) Vector2.Distance(this.transform.position, this.targetPos);
        float ratio =   (float)passedDistance / (float)allDistance;
 
        float allDistanceY = Mathf.Abs(this.targetPos.y - this.pos.y);
        float passedDistanceY = allDistanceY - Mathf.Abs(this.targetPos.y - this.transform.position.y);

 
        float ratioY = passedDistanceY / allDistanceY;

        float scaleY = (myVideoController.stepTillTarget * ratio) * this.stepYSizeScale;
        float returnVal = this.currentScale.y + (lead) * scaleY;

        if (lead == 1)
        {
            if (returnVal >= this.targetScale.y)
                return targetScale.y;
        }
        else if (lead == -1)
        {
            if (returnVal <= this.targetScale.y)
                return targetScale.y;
        }
        return returnVal;
    }

    public void linking()
    {
        if (!linked)
        {
            this.allListElements = FindObjectsOfType<listElement>();
            for (int i = 0; i < this.allListElements.Length; i++)
            {
                if (this.row == this.allListElements[i].row)
                {
                    if (this.index == this.allListElements[i].index - 1)
                        this.next = this.allListElements[i];
                    if (this.index == this.allListElements[i].index + 1)
                        this.previous = this.allListElements[i];
                    if (this.index == 0)
                    {
                        if (this.maxCountInRow == this.allListElements[i].index + 1)
                        {
                            this.previous = this.allListElements[i];
                        }
                    }

                    if (this.index == this.maxCountInRow - 1)
                    {
                        if (this.allListElements[i].index == 0)
                        {
                            this.next = this.allListElements[i];
                        }
                    }
                }
            }
            if (this.next != null && this.previous != null)
                this.linked = true;
        }
    }

    public void initListElement(int index, int row, int maxCount, Vector2 position, Vector2 scale)
    {
        this.setNumbers(index, row,maxCount);
        this.setPos(position);
        this.setTargetPos(position);
        this.currentScale = scale;
    }

    public void setNumbers(int index, int row, int maxCount)
    {
        this.index = index;
        this.row = row;
        this.maxCountInRow = maxCount;
        this.posIndex = this.index;
    }
    public void setPos(Vector2 position)
    {
        this.pos = position;
    }

    public void setTargetPos(Vector2 targetPosition)
    {
        this.targetPos = targetPosition;

    }

    public void calcStepSize()
    {
        float distanceToTarget = Vector2.Distance(this.pos,this.targetPos);
        float scaleXDiff = Mathf.Abs(this.targetScale.x - this.currentScale.x);
        float scaleYDiff = Mathf.Abs(this.targetScale.y - this.currentScale.y);

        int stepToReachTarget = (int) Mathf.Ceil(distanceToTarget / this.stepSize);


        this.stepXSizeScale = stepToReachTarget == 0 ? 0 : scaleXDiff / stepToReachTarget;
        this.stepYSizeScale = stepToReachTarget == 0 ? 0 : scaleYDiff / stepToReachTarget;
    }



 





    public int newPosIndex = 0;
    public int oldPosIndex = 0;
   

    


}
