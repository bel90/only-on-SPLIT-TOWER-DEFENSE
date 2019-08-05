using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour {

    public static PanelManager instance;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject startPanel;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Congratulation!\n\nThere are " + GameManager.instance.GetEnemyCount()
            + " Enemies on the field!\n\nCan You beat your own highscore?";
        Time.timeScale = 0f;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void StartGame() {
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
