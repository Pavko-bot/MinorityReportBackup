using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class VideoVariables : MonoBehaviour
{
    public bool IsRelevant { get; private set; }
    public string Type { get; private set; }
    public double StartTimeHighlight { get; private set; }
    public double EndTimeHighlight { get; private set; }
    public string Voiceline { get; private set; }
    public string Name { get => this.transform.name; }
    public double CurrentTime { get => this.transform.GetComponent<VideoPlayer>().time; }
    public double CurrentClockTime { get => this.transform.GetComponent<VideoPlayer>().clockTime; }
    public double CurrentPercentage { get => (double)this.transform.GetComponent<VideoPlayer>().frame / this.transform.GetComponent<VideoPlayer>().frameCount; 
        set => this.transform.GetComponent<VideoPlayer>().frame = (long)(value * this.transform.GetComponent<VideoPlayer>().frameCount); }
    public double CurrentSeconds { get => (transform.GetComponent<VideoPlayer>().frame / this.transform.GetComponent<VideoPlayer>().frameRate); }
    public long Frame { get => transform.GetComponent<VideoPlayer>().frame; }
    public float FrameRate { get => transform.GetComponent<VideoPlayer>().frameRate; }
    public bool HasBeenFound
    {
        get => _hasBeenFound;
        set => _hasBeenFound |= value;
    }
    private bool _hasBeenFound;

    public RenderTexture texture;

    public void InitVideo(string folderName, UserData _userData)
    {
        IsRelevant = _userData.IsRelevant;
        Type = _userData.Type;
        StartTimeHighlight = _userData.StartTimeHighlight;
        EndTimeHighlight = _userData.EndTimeHighlight;
        Voiceline = _userData.Voiceline;
        this.transform.name = _userData.Video;
        this.transform.GetComponent<VideoPlayer>().url = $"{folderName}/{_userData.Video}.mp4";
        texture = new RenderTexture(1280, 720, 16);
        this.transform.GetComponent<RawImage>().texture = texture;
        this.transform.GetComponent<VideoPlayer>().targetTexture = texture;
        //this.transform.GetComponent<VideoPlayer>().Pause();
    }
}
