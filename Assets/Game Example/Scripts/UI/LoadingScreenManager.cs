using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText; // Assign your Text UI element in the Inspector
    public GameObject Score;
    public GameObject loadingScreen;
    public GameObject dialogBoxObject;
    public bool gameStart = false;
    private int tasksRemaining = -1;

    private void Awake()    
    {
        Score.SetActive(false);
        gameStart = false;
    }
    public void RegisterTask()
    {
        Debug.Log("asda");
        tasksRemaining++;
    }

    public void TaskCompleted()
    {
        tasksRemaining--;

        if (tasksRemaining <= 0 && loadingScreen != null)
        {
            StaticVariableManager.intro = false;
            Score.SetActive(true);
            loadingScreen.SetActive(false);
            gameStart = true;
        }
    }
    private void Start()
    {
        dialogBoxObject.SetActive(true);
    }
    public void Update()
    {
        scoreText.text = $"{StaticVariableManager.score}";
    }
}
