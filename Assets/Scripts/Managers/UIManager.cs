using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonobehaviourSingleton<UIManager>
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject winCanvasObject;
    [SerializeField] private GameObject loseCanvasObject;

    public void UpdateScoreText(int _totalScore)
    {
        scoreText.text = _totalScore.ToString();
    }

    public void ShowWinUI()
    {
        winCanvasObject.SetActive(true);
    }

    public void ShowLoseUI()
    {
        loseCanvasObject.SetActive(true);
    }

    private void Start()
    {
        GameManager.Instance.scoreUpdatedEvent += UpdateScoreText;
    }

    private void OnDisable()
    {
        GameManager.Instance.scoreUpdatedEvent -= UpdateScoreText;
    }
}
