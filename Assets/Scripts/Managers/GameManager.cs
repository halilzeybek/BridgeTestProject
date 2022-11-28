using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonobehaviourSingleton<GameManager>
{
    public event Action<int> scoreUpdatedEvent;
    public string currentLevelName;

    [SerializeField] private int winScoreLimit = 400;
    [SerializeField] private int nextLevelScoreLimit = 100;

    private void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
    }

    #region EventMethods
    private void OnEnable()
    {
        scoreUpdatedEvent += CheckWinCondition;
    }

    private void OnDisable()
    {
        scoreUpdatedEvent -= CheckWinCondition;
    }

    public void FireScoreUpdateEvent(int _totalScore)
    {
        scoreUpdatedEvent?.Invoke(_totalScore);
    }
    #endregion


    public void CheckWinCondition(int _totalScore)
    {
        if(_totalScore >= winScoreLimit)
        {
            Win();
        }
        else if(_totalScore >= nextLevelScoreLimit)
        {
            StartNextLevel();
        }
    }

    private void Win()
    {
        UIManager.Instance.ShowWinUI();
        SaveManager.Save(ScoreManager.Instance.score);
    }

    public void Lose()
    {
        UIManager.Instance.ShowLoseUI();
    }

    private void StartNextLevel()
    {
        SaveManager.Save(ScoreManager.Instance.score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
