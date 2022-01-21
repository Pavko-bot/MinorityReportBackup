using System;

[Serializable]
public class UserData
{
    public string Name;
    public bool IsRelevant;
    public string Type;
    public float StartTimeHighlight;
    public float EndTimeHighlight;
    public string Voiceline;
    public string Video;

    public UserData()
    {
        IsRelevant = false;
        Type = "";
        StartTimeHighlight = -1f;
        EndTimeHighlight = -1f;
        Name = "";
        Voiceline = "";
        Video = "";
    }

    public override string ToString()
    {
        return $"UserData: IsRelevant:{IsRelevant} Type:{Type} Name:{Name} StartTimeHighlight:{StartTimeHighlight} EndTimeHighlight:{EndTimeHighlight} Voiceline:{Voiceline} Video:{Video}";
    }
}
