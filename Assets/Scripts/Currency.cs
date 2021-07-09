using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] private int startBalance;
    [SerializeField] private int currentBalance;
    [SerializeField] private TextMeshProUGUI text;

    public int CurrentBalance => currentBalance;


    // Start is called before the first frame update
    void Awake()
    {
        currentBalance = startBalance;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText()
    {
        text.text = currentBalance.ToString();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateText();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if (currentBalance < 0)
        {
            ReloadScene(); //lose the game
        } else if (currentBalance > 500)
        {
            
        }
        UpdateText();
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
