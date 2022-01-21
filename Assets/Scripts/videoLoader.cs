using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class videoLoader : MonoBehaviour
{
    public string FolderName;
    public GameObject VideoPrefab;
    public List<GameObject> videoObjects;

    private List<UserData> userDataCollection;
    private VideoController myVideoController;

    public float offsetXBigVideos = 4.5f;
    public float offsetXSmallVideos = 4.5f;

    public int videosAsBorderBigScale = 3;
    public int videosAsBorderSmallScale = 3;

    public int videosTillBorderSmallScale = 6;
    public int videosTillBorderBigScale = 6;

    public int mainVideoIndex = 6;

    private void Awake()
    {
        myVideoController = GetComponent<VideoController>();
        this.userDataCollection = ReadUserData(FolderName);
        InstantiateVideos(out this.videoObjects, this.userDataCollection);
    }

    private List<UserData> ReadUserData(string folderName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(folderName);
        FileInfo[] metaCollection = dirInfo.GetFiles("*.userData");
        List<UserData> userDataCollection = new List<UserData>();
        foreach (FileInfo meta in metaCollection)
        {
            StreamReader streamReader = meta.OpenText();
            string deserializedString = streamReader.ReadToEnd();
            UserData userData = JsonUtility.FromJson<UserData>(deserializedString);
            if (userData == null)
            {
                Debug.LogError($"Cannot parse Json of file {meta.FullName}: {deserializedString}");
            }
            userDataCollection.Add(userData);
        }
        return userDataCollection;
    }


    private void InstantiateVideos(out List<GameObject> videos, List<UserData> userDataCollection)
    {
        videos = new List<GameObject>();

        int tempCount = userDataCollection.Count;
        int spawnedVideos = 0;

        for (int currentRow = 3; currentRow > 0; currentRow--)
        {
            int rowCount = tempCount / currentRow;
            myVideoController.initPositions(currentRow, rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                int x = i % rowCount;
                int y = currentRow - 1;
                Vector2 position = new Vector2(0, 0);
                Vector2 scale = myVideoController.smallScale;
                if (y== 1)
                {
                    position = myVideoController.calculatePositions(x, y,
                        myVideoController.bigScale, (int)(myVideoController.bigScale.x * videosTillBorderBigScale),
                        videosAsBorderBigScale, (int)(-offsetXBigVideos * myVideoController.bigScale.x), -2 * (int)myVideoController.smallScale.y, false, 0, 2);

                    // (int)(-myVideoLoader.offsetXBigVideos * bigScale.x), -2 * (int)smallScale.y, false, 0, mainVideoRow);

                    scale = myVideoController.bigScale;
                    if(i == this.mainVideoIndex)
                    {
                        position = new Vector2(0, 0);
                        scale = myVideoController.mainVideoScale;
                    }
                }
                else
                {
                    position = myVideoController.calculatePositions(x, y,
                        myVideoController.smallScale, (int) (videosTillBorderSmallScale * myVideoController.smallScale.x), videosAsBorderSmallScale, (int) (myVideoController.smallScale.x  * - offsetXSmallVideos), -405, true, y);

                    scale = myVideoController.smallScale;
                }



                GameObject video = Instantiate(this.FolderName, userDataCollection[spawnedVideos], x, currentRow, rowCount,position,scale);
                //init main video
                if (i == mainVideoIndex && y == 1)
                {
                    video.GetComponent<listElement>().isMainVideo = true;
                }
                    

                spawnedVideos++;
                videos.Add(video);
                myVideoController.setPositions(currentRow, i,position);
            }
            tempCount -= rowCount;
            
        }
    }

    private GameObject Instantiate(string folder, UserData userData, int index, int row, int maxCount, Vector2 position, Vector2 scale)
    {
        GameObject video = Instantiate(VideoPrefab, this.transform);
        RectTransform rectTransform = video.GetComponent<RectTransform>();
        rectTransform.sizeDelta = scale;

        if (position != null)
        {
            video.transform.position = (Vector2)position;
        }
        video.GetComponent<VideoVariables>().InitVideo(folder, userData);
        video.GetComponent<listElement>().initListElement(index, row, maxCount,position,scale);
        if (row == 2)
        {
            video.GetComponent<listElement>().isActiveRow = true;
        }

        return video;
    }
}
