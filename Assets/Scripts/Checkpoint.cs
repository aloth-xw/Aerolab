using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;

    [SerializeField] private MeshRenderer ringRenderer;
    [SerializeField] private UnityEngine.Color activeColor = UnityEngine.Color.yellow;
    [SerializeField] private UnityEngine.Color passedColor = Color.green;

    private bool isTriggered = false;
    
    void Start()
    {
        SetRingColor(activeColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Player") || other.GetComponentInParent<Aircraft>() != null)
        {
            isTriggered = true;

            SetRingColor(passedColor);

            if (tutorialManager != null)
            {
                tutorialManager.OnCheckpointReached();
            }

            GetComponent<Collider>().enabled = false;
        }
    }

    private void SetRingColor(Color color)
    {
        if (ringRenderer == null || ringRenderer.material == null) return;

        if (ringRenderer.material.HasProperty("_BaseColor"))
        {
            ringRenderer.material.SetColor("_BaseColor", color);
        }
        else
        {
            ringRenderer.material.color = color;
        }
    }
}
