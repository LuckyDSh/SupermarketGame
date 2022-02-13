/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAME_CONTROLLER : MonoBehaviour
{
    #region Variables
    public static GAME_CONTROLLER instance;
    [SerializeField] private GameObject GW_UI;
    [SerializeField] private GameObject HELLO_UI;
    [SerializeField] private Text score_txt;
    [SerializeField] private Text GW_Menu_score_txt;
    public static bool IS_PLAYING;
    private int score;
    private float multiplierValue;
    private bool isFirstLaunch = true;

    private readonly int FIRST = 0;
    private readonly int SECOND = 1;
    private readonly int THIRD = 2;

    public static bool IS_GAME_WIN;
    public static bool IS_GAME_LOSE;

    [Range(0,20)]
    [SerializeField] private float time_stamp;
    #endregion

    #region UnityMethods

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        score = 0;
        multiplierValue = 0;
        GW_UI.SetActive(false);
        HELLO_UI.SetActive(true);
        IS_PLAYING = false;
        Time.timeScale = time_stamp;
    }

    void Update()
    {
        //if (PlayerStackColorController.atEnd)
        //{
        //    if (IS_GAME_WIN)
        //        // A bit pure optimization level (can be modified)
        //        if (EnemyController.rb_buffer.IsSleeping())
        //        {
        //            GW_UI.SetActive(true);
        //            DisplayScore();
        //        }
        //}
    }
    #endregion

    private void GW()
    {
        GW_UI.SetActive(true);
        DisplayScore();
    }

    internal void UpdateMultiplier(float newValue)
    {
        if (multiplierValue >= newValue)
            return;
        multiplierValue = newValue;
        score = (int)(score * multiplierValue);
        score_txt.text = score.ToString();
    }

    public void DisplayScore()
    {
        GW_Menu_score_txt.text = score.ToString();
    }

    public void UpdateScore(int value)
    {
        score += value;
        if (score < 0)
            score = 0;
        score_txt.text = score.ToString();
    }

    public void START()
    {
        IS_PLAYING = true;
        HELLO_UI.SetActive(false);
        WavesGenerator.wave_isOn = true;
        WavesGenerator.boss_isOn = true;
        //Enemy.is_moving = true;
    }

    public void LOAD_SECOND_SCENE()
    {
        SceneManager.LoadScene(SECOND);
    }

    public void RESTART()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EXIT()
    {
        Application.Quit();
    }
}
