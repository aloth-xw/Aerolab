using UnityEngine;

public enum TutorialStepType
{
    ShowMessage,
    RequireInput,
    RequireCheckpoint
}

[System.Serializable]
public class TutorialStep
{
    public TutorialStepType type;
    [TextArea(2,4)] public string message;
    //public Checkpoint targetCheckpoint;
    public bool freezeAircraft = false;
}
