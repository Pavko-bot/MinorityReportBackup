using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class VideoController : MonoBehaviour
{
    public Vector2 mainVideoScale = new Vector2(1920, 1080);
    public Vector2 standardScale = new Vector2(640, 360);
    public Vector2 bigScale = new Vector2(960, 540);
    public Vector2 smallScale = new Vector2(480, 270);

    public int stepTillTarget = 10;

    public Vector2[] positions_1, positions_2, positions_3;

    public videoLoader myVideoLoader;


    public int mainVideoIndex = 0;


    private void Start()
    {
        myVideoLoader = GetComponent<videoLoader>();
        mainVideoIndex = myVideoLoader.mainVideoIndex;
        
    }

    public void initPositions(int rowIndex, int rowCount)
    {
        if(rowIndex == 1)
        {
            this.positions_1 = new Vector2[rowCount];
        }
        if (rowIndex == 2)
        {
            this.positions_2 = new Vector2[rowCount];
        }
        if (rowIndex == 3)
        {
            this.positions_3 = new Vector2[rowCount];
        }
    }


    public void setPositions(int rowIndex, int index, Vector2 position, bool activeRow = true)
    {

        if (rowIndex == 1)
        {
            this.positions_1[index] = position;
        }
        if (rowIndex == 2)
        {
            this.positions_2[index] = position;
        }
        if (rowIndex == 3)
        {
            this.positions_3[index] = position;
        }
    }

    public Vector2 calculatePositions(int x, int y, Vector2 scale, int border, int videosTilBorder, int xOffset, int yOffset,bool smallRow = false, int row = 0, int mainVideoRow = 0)
    {


        Vector2 pos = new Vector2(x * scale.x, y * scale.y);
        if (x * scale.x + scale.x > border)
        {
            pos.x += videosTilBorder * scale.x;
        }

        pos.x += xOffset;
        pos.y += yOffset ;


        if (smallRow)
        {
            switch (row)
            {
                case 2: //middle row -> upper row has to be shifted up by its y scale
                    if (y == 2)
                        pos.y += scale.y;
                    break;
                case 1: //bottom row -> upper row and middle row have to be shifted by y scale
                    if (y == 2 || y == 1)
                        pos.y += scale.y;
                    break;
            }
        }
        
        if (mainVideoRow != 0)
        {
   

            if (mainVideoRow - 1 == y)
            {
             
                //Debug.Log("I am here" + mainVideoRow + " " + y);
                if (x > 6)

                {
                    //Debug.Log(x);
                    pos.x -= scale.x;
                }

            }
        }
        //else //dont do in default mode
        //{
        //    if(x == 4)
        //        pos = new Vector2(0, 0);

        //    if(x > 4)
        //    {
        //        pos.x -= 2 * scale.x;
        //    }
        //}

        return pos;
    }



    public void rePositionVideos(int rowIndex)
    {
        
        int mainVideoRow = 0;
        foreach (GameObject element in myVideoLoader.videoObjects)
        {
            listElement video = element.GetComponent<listElement>();

            if (video.isMainVideo)
            {
                mainVideoRow = video.row;
                Vector2 posi = new Vector2(0, 0);
                this.setPositions(video.row, video.posIndex, posi);
                video.setTargetPos(posi);
                continue;
            }
        }
        if (rowIndex == 1)
        {

            foreach (GameObject element in myVideoLoader.videoObjects)
            {
                listElement video = element.GetComponent<listElement>();
                if (video.isMainVideo)
                    continue;
                if (video.row == rowIndex)
                {
                    //Debug.Log(video.index);
                    Vector2 posi = this.calculatePositions(video.posIndex, video.row - 1, this.bigScale, (int)(bigScale.x * myVideoLoader.videosTillBorderBigScale), myVideoLoader.videosAsBorderBigScale, (int)(-myVideoLoader.offsetXBigVideos * bigScale.x), -(int)smallScale.y,false,0,mainVideoRow);
                    this.setPositions(video.row,video.posIndex, posi);
                    video.setTargetPos(posi);
                    continue;
                }
                else
                {
                    Vector2 pos = this.calculatePositions(video.posIndex, video.row - 1, this.smallScale, (int)(myVideoLoader.videosTillBorderSmallScale * smallScale.x), myVideoLoader.videosAsBorderSmallScale, (int) (-smallScale.x * myVideoLoader.offsetXSmallVideos), -405, true, rowIndex, mainVideoRow);
                    this.setPositions(video.row, video.posIndex, pos, false);
                    video.setTargetPos(pos);
                }

                
            }
        }
        if (rowIndex == 2)
        {

            foreach (GameObject element in myVideoLoader.videoObjects)
            {
                listElement video = element.GetComponent<listElement>();
                if (video.isMainVideo)
                    continue;
                if (video.row == rowIndex)
                {
                    Vector2 posi = this.calculatePositions(video.posIndex, video.row - 1, this.bigScale, (int)(bigScale.x * myVideoLoader.videosTillBorderBigScale), myVideoLoader.videosAsBorderBigScale, (int)(-myVideoLoader.offsetXBigVideos * bigScale.x), -2 * (int)smallScale.y, false, 0, mainVideoRow);
                    this.setPositions(video.row, video.posIndex, posi);
                    video.setTargetPos(posi);
                    continue;
                }
                else
                {
                    Vector2 pos = this.calculatePositions(video.posIndex, video.row - 1, this.smallScale, (int)(myVideoLoader.videosTillBorderSmallScale * smallScale.x), myVideoLoader.videosAsBorderSmallScale, (int)(-smallScale.x * myVideoLoader.offsetXSmallVideos), -405, true, rowIndex, mainVideoRow);
                    this.setPositions(video.row, video.posIndex, pos, false);
                    video.setTargetPos(pos);

                }
                
            }
        }
        if (rowIndex == 3)
        {

            foreach (GameObject element in myVideoLoader.videoObjects)
            {
                listElement video = element.GetComponent<listElement>();
                if (video.isMainVideo)
                    continue;
                if (video.row == rowIndex)
                {
                    Vector2 posi = this.calculatePositions(video.posIndex, video.row - 1, this.bigScale, (int)(bigScale.x * myVideoLoader.videosTillBorderBigScale), myVideoLoader.videosAsBorderBigScale, (int)(-myVideoLoader.offsetXBigVideos * bigScale.x), -3 * (int)smallScale.y, false, 0, mainVideoRow);
                    this.setPositions(video.row, video.posIndex, posi);
                    video.setTargetPos(posi);
                    continue;
                }
                else
                {
                    Vector2 pos = this.calculatePositions(video.posIndex, video.row - 1, this.smallScale, (int)(myVideoLoader.videosTillBorderSmallScale * smallScale.x), myVideoLoader.videosAsBorderSmallScale, (int)(-smallScale.x * myVideoLoader.offsetXSmallVideos), -405, true, rowIndex, mainVideoRow);
                    this.setPositions(video.row, video.posIndex, pos, false);
                    video.setTargetPos(pos);
                }

            }
        }

    }

    public void resetVideoPositions()
    {
        foreach (GameObject element in myVideoLoader.videoObjects)
        {
            listElement video = element.GetComponent<listElement>();
            Vector2 scale, position;
            this.setDefaultPositions(video.posIndex, video.row - 1,out scale,out position);
            this.setPositions(video.row, video.index, position);
            video.setTargetPos(position);
        }
    }

    public void setDefaultPositions(int index,int row,out Vector2 scale,out Vector2 position)
    {

        if (row == 1)
        {
            position = this.calculatePositions(index, row, this.bigScale, (int)(bigScale.x * 5.5f), 3, (int)(-5.5 * bigScale.x), -(int)smallScale.y);
            scale = new Vector2(this.bigScale.x, this.bigScale.y);
        }
        else
        {
            position = this.calculatePositions(index, row, this.smallScale, 2200, 4, -2640, -405, true, index);
            scale = new Vector2(this.smallScale.x, this.smallScale.y);
        }
        

    }




}
