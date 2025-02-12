using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxLegacy : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.Text dialogueText;          // Legacy Text component for displaying dialogue
    public GameObject dialogueBoxPanel; // Panel containing the dialogue UI elements

    [Header("Dialogue Settings")]
    [TextArea(3, 10)]
    public string[] dialogueLines;      // Array of dialogue lines to display
    public float typingSpeed = 0.05f;   // Speed of typing effect

    private int currentLineIndex = 0;   // Tracks the current line in dialogue
    private bool isTyping = false;      // Checks if text is currently being typed

    public LoadingScreenManager loadingScreenManager;
    private void Start()
    {
        dialogueBoxPanel.SetActive(false); // Hide dialogue box initially
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;           // Set the dialogue lines
        currentLineIndex = 0;            // Reset to the first line
        dialogueBoxPanel.SetActive(true); // Show the dialogue box
        DisplayNextLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueBoxPanel.activeSelf)
        {
            AdvanceDialogue(); // Advance dialogue on mouse click
        }
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        }
        else
        {
            EndDialogue(); // End dialogue if no more lines
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear the text   
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void AdvanceDialogue()
    {
        if (isTyping)
        {
            // Skip typing and display full line instantly
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
        }
        else
        {
            currentLineIndex++; // Move to the next line
            DisplayNextLine();
        }
    }

    public void EndDialogue()
    {
        dialogueBoxPanel.SetActive(false); // Hide dialogue box
    }
}
