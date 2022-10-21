using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] 
    private int maxLives = 5;

    [SerializeField] 
    private Text livesText;
    private int lives;
    [SerializeField] 
    private Text scoreText;
    private float score = 0;
    
    [SerializeField] 
    private GameObject failed;

    [SerializeField] 
    private GameObject done;
    
    public GameObject panel;
    public GameObject play;
    public GameObject pause;
    public GameObject restart;
    internal void UpdateLives()
    {
        --lives;
        livesText.text = $"LIVES: {lives}";
        if (lives == 0)
        {
            TriggerGameOver();
        }
    }
    
    internal void UpdateScore(float value)
    {
        score += value;
        scoreText.text = $"SCORE: {score}";
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        restart.SetActive(false);
        panel.SetActive(false);
        play.SetActive(false);
        failed.SetActive(false);
        done.SetActive(false);
        lives = maxLives;
        livesText.text = $"LIVES: {lives}";
        scoreText.text = $"SCORE: {score}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Quit();
        }
    }
    
    internal void TriggerGameOver(bool failure = true)
    {
        failed.SetActive(failure);
        done.SetActive(!failure);
        pause.SetActive(false);
        restart.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Play()
    {
        panel.SetActive(false);
        play.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1f;
    }
    
    public void Pause()
    {
        panel.SetActive(true);
        pause.SetActive(false);
        play.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
