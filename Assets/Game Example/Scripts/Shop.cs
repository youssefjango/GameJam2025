using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Shop : MonoBehaviour
{
    public UnityEngine.UI.Text upgrade1Text, upgrade2Text, upgrade3Text; // UI Text for upgrade costs
    public Button upgrade1Button, upgrade2Button, upgrade3Button; // Buttons
    public AudioSource audioSource;

    private void Start()
    {
        UpdateUI();
    }

    public void Upgrade1()
    {
        audioSource.Play();
        float cost = StaticVariableManager.upgradeHp * 30f;
        if (StaticVariableManager.money >= cost)
        {
            StaticVariableManager.money -= (int)cost;
            StaticVariableManager.upgradeHp++;
            UpdateUI();
        }
    }

    public void Upgrade2()
    {
        audioSource.Play();
        float cost = StaticVariableManager.upgradeDmg * 30f;
        if (StaticVariableManager.money >= cost)
        {
            StaticVariableManager.money -= (int)cost;
            StaticVariableManager.upgradeDmg++;
            UpdateUI();
        }
    }

    public void Upgrade3()
    {
        audioSource.Play();
        float cost = StaticVariableManager.upgradeMp * 30f;
        if (StaticVariableManager.money >= cost)
        {
            StaticVariableManager.money -= (int)cost;
            StaticVariableManager.upgradeMp++;
            UpdateUI();
        }
    }
    public void Upgrade4()
    {
        audioSource.Play();
        float cost = 1000f;
        if (StaticVariableManager.money >= cost)
        {
            StaticVariableManager.money -= (int)cost;
            SceneManager.LoadSceneAsync(0);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        upgrade1Text.text = "Cost: " + StaticVariableManager.upgradeHp*30f;
        upgrade2Text.text = "Cost: " + StaticVariableManager.upgradeDmg * 30f;
        upgrade3Text.text = "Cost: " + StaticVariableManager.upgradeMp * 30f;
    }
}
