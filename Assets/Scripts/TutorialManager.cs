using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialStep[] steps;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private GameObject continuePrompt;
    [SerializeField] private Rigidbody aircraftRigidbody;

    private int currentStep = 0;
    private bool waitingForContinue = false;

    private void Start()
    {
        ShowStep(0);
    }

    private void ShowStep(int index)
    {
        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        currentStep = index;
        TutorialStep step =steps[index];
        tutorialText.text = step.message;
        tutorialText.gameObject.SetActive(true);

        if (step.freezeAircraft)
        {
            aircraftRigidbody.linearVelocity = Vector3.zero;
            aircraftRigidbody.angularVelocity = Vector3.zero;
            aircraftRigidbody.isKinematic = true;
        }

        if (step.type == TutorialStepType.ShowMessage)
        {
            waitingForContinue = true;
            continuePrompt.SetActive(true);
        }
        else if (step.type == TutorialStepType.RequireCheckpoint)
        {
            aircraftRigidbody.isKinematic = false;
        }
    }

    private void Update()
    {
        if (waitingForContinue && UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            waitingForContinue = false;
            continuePrompt.SetActive(false);
            aircraftRigidbody.isKinematic = false;
            ShowStep(currentStep + 1);
        }
    }

    public void OnCheckpointReached()
{
    if (steps[currentStep].type == TutorialStepType.RequireCheckpoint)
    {
        ShowStep(currentStep + 1);
    }
}

    private void EndTutorial()
    {
        tutorialText.gameObject.SetActive(false);
    }

}
