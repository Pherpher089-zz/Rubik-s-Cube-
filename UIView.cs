using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIView : RubiksView {

    public GUISkin timeGUISkin;
    Canvas startCanvas;
    Text timerText;
    float gameTime;
    string viewTime;
    string viewTimer;
    bool showMsg;
    bool restart = false;
    bool quit = false;

    private void Awake()
    {
        startCanvas = GameObject.FindWithTag("StartCanvas").GetComponent<Canvas>();
        timerText = GameObject.FindWithTag("TimeText").GetComponent<Text>();
    }

    private void Update()
    {
        if (app.model.gameState == GameState.Start)
        {
            startCanvas.enabled = true;
        }
        else
        {
            startCanvas.enabled = false;
        }

        if (restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(quit)
        {
            Application.Quit();
        }

        gameTime = app.model.timer;
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);

        
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        
    }

    private void OnGUI()
    {

        GUI.skin.textArea.alignment = TextAnchor.MiddleCenter;
        GUI.color = Color.white;
        GUI.skin.textArea.fontSize = 42;
        GUI.backgroundColor = Color.white;
        GUIStyle endGameStyle = new GUIStyle();
        endGameStyle.fontSize = 72;
        endGameStyle.alignment = TextAnchor.MiddleCenter;

        gameTime = app.model.timer - Time.time;
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        viewTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (app.model.gameState == GameState.Play)
        {
            GUI.TextArea(new Rect(50, 50, 300, 50), "Time: " + viewTime);
        }

        if (app.model.gameState == GameState.Win)
        {
            GUI.Box(new Rect(870, 440, 300, 100), "You Win!", endGameStyle);
            GUI.TextArea(new Rect(870, 440, 300, 100), "You Lose..", endGameStyle);
            restart = GUI.Button(new Rect(870, 550, 100, 35), "Restart");
        }

        if (app.model.gameState == GameState.Loose)
        {
            GUI.TextArea(new Rect(870, 440, 300, 100), "You Lose..", endGameStyle);
            restart = GUI.Button(new Rect(870, 550, 100, 35),"Restart");
        }

        if(showMsg)
        {
            GUI.Box(new Rect(870, 440, 300, 100), "Start!", endGameStyle);
            StartCoroutine(ShowMsg());
        }
    }

    IEnumerator ShowMsg()
    {
        yield return new WaitForSeconds(1);
        showMsg = false;
    }

    public void UpTime()
    {
        app.model.timer += 15;
    }

    public void DownTime()
    {
        if (app.model.timer > 0)
        {
            app.model.timer -= 15;
        }
    }

    public void StartGame()
    {
        app.model.gameState = GameState.Shuffle;
        showMsg = true;
    }

    public void QuitGame()
    {
        if (app.model.gameState == GameState.Play || app.model.gameState == GameState.Shuffle)
        {
            app.model.gameState = GameState.Start;
        }
        else if (app.model.gameState == GameState.Start)
        {
            Application.Quit();
        }
        showMsg = true;
    }

}
