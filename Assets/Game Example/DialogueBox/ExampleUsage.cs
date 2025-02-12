using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBoxLegacy dialogueBox;
    public LoadingScreenManager loadingScreenManager;
    private void Start()
    {
        if (loadingScreenManager != null)
        {
            string[] lines = {
            "You set out on what should have been a simple errand of fetching 15 crystals for your master. " +
            "The list was tucked safely into your pouch. The path through the woods was familiar. ",
            "Suddenly, the forest floor shakes...",
            "The ground crumbled beneath you, and before you could react, you were tumbling into darkness. "
        };

            dialogueBox.StartDialogue(lines);
        }
        else
        {
            string[] lines = {
            "Fetch me some crystlas from the hole"
        };

            dialogueBox.StartDialogue(lines);
        }
    }
}
