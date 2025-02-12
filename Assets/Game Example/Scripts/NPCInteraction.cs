using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;                      // Reference to the player's transform
    public Transform npc;                         // Reference to the NPC's transform
    public CinemachineVirtualCamera virtualCamera; // Reference to the virtual camera
    public float interactionRange = 3f;            // Distance to trigger interaction
    public DialogueBoxLegacy dialogueBox;                // Dialogue box UI
    public GameObject buyMenu;                    // Buy menu UI
    public Text dialogueText;                     // Text for the dialogue box
    private bool isDialoging = false;
    private bool isInRange = false;               // Whether the player is within interaction range
    public GameObject CoinPrefab;
    private void Start()
    {
        ShowBuyMenu(false);
    }
    void Update()
    {
        // Check if the player is close to the NPC
        float distanceToNPC = Vector3.Distance(player.position, npc.position);

        if (distanceToNPC <= interactionRange)
        {
            if (!isInRange)
            {
                // Player enters interaction range
                isInRange = true;
            }
            if (!isDialoging)
            {
                isDialoging = true;
                StartDialogue();
            }
            ShowBuyMenu(true);
            SwitchCameraTarget();

        }
        else if (distanceToNPC >= interactionRange) {
        
            if (isInRange)
            {
                dialogueBox.EndDialogue();
                isDialoging = false;
                // Player exits interaction range
                isInRange = false;
                ShowBuyMenu(false);
                virtualCamera.Follow = player;  // Change the follow target to the NPC

            }
        }
    }

    // Switch the camera's follow target to the NPC
    void SwitchCameraTarget()
    {
        virtualCamera.Follow = npc;  // Change the follow target to the NPC
    }

    // Display the dialogue box and start the dialogue
    void StartDialogue()
    {
        if (StaticVariableManager.intro)
        {
            string[] lines = {
            "Aprentice! Fetch me some crystals from the hole"
            };
            dialogueBox.StartDialogue(lines);
        }
        else
        {
            string[] lines = {
            "I have everything you will ever need"
            };
            StartCoroutine(InstantiateObjectsWithInterval(StaticVariableManager.score * 5, CoinPrefab, 0.05f));
            dialogueBox.StartDialogue(lines);
            StaticVariableManager.money += StaticVariableManager.score * 5;
            StaticVariableManager.score = 0;
        }
    }

    // Hide the dialogue box when interaction is finished

    // Show or hide the buy menu
    void ShowBuyMenu(bool show)
    {
        buyMenu.SetActive(show);
    }
    IEnumerator InstantiateObjectsWithInterval(int numberOfInstantiations, GameObject objectToInstantiate, float interval)
    {
        for (int i = 0; i < numberOfInstantiations; i++)
        {
            // Instantiate the object at the current position with the default rotation
            Instantiate(objectToInstantiate, transform.position, Quaternion.identity);

            // Wait for the specified interval before continuing the loop
            yield return new WaitForSeconds(interval);
        }
    }
    // Display a simple prompt when close to NPC
}
