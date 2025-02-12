using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndgameManager : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText; // Assign your Text UI element in the Inspector
    public UnityEngine.UI.Text timerText; // Assign your Text UI element in the Inspector
    public UnityEngine.UI.Text overallText; // Assign your Text UI element in the Inspector
    public Image statusImage; // The Image UI element you want to change
    public Sprite goodEndSprite; // Sprite for good ending
    public Sprite badEndSprite; // Sprite for bad ending
    public float overall;

    // Method to set the final score
    public void SetFinalScore()
    {
        Debug.Log(StaticVariableManager.isDead);
        
        if (StaticVariableManager.isDead) // Example condition for a good ending
        {
            statusImage.sprite = badEndSprite;  // Change to good ending sprite
            overall = 0;
        }
        else
        {
            overall = (StaticVariableManager.score * 30) - StaticVariableManager.timer;
            statusImage.sprite = goodEndSprite; // Change to bad ending sprite
        }
        UpdateScoreText(); // Update the text UI

    }

    // Method to update the text UI with the final score
    private void UpdateScoreText()
    {

        if (scoreText != null)
        {
            scoreText.text = $"Collected Crystals: {StaticVariableManager.score}";
            timerText.text = $"Total Time: {StaticVariableManager.timer}";
            overallText.text = $"Overall Score: {overall}";
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the EndgameManager.");
        }
    }

    // Example trigger for ending the game
    public void EndGame()
    {
        // Replace with your endgame logic
        Debug.Log("Game Over!");
        SetFinalScore();
    }

    private void Start()
    {
        EndGame();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadSceneAsync(0); // Load the main menu scene
    }
}
