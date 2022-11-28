using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonobehaviourSingleton<ScoreManager>
{
    public Score score = new Score();

    private ShapeType lastCollectedType = ShapeType.none;
    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        // Transfer the score to the new level
        UIManager.Instance.UpdateScoreText(score.totalScore);
    }
    public void AddScore(int _score, ShapeType _shapeType)
    {
        // Checks if player collects same shape again
        if ( lastCollectedType == _shapeType)
        {
            DecreaseScore(_score);
            return;
        }

        lastCollectedType = _shapeType;

        score.totalScore += _score;

        int _currentShapeCount;

        if (score.shapeScoreDic.TryGetValue(_shapeType, out _currentShapeCount))
        {
            // Shape is exist in dict, increase the value of it.
            score.shapeScoreDic[_shapeType]++;
        }
        else
        {
            // No key found, add new one.
            score.shapeScoreDic.Add(_shapeType, 1);
        }

        GameManager.Instance.FireScoreUpdateEvent(score.totalScore);
    }

    public void DecreaseScore(int _scoreToDecrease)
    {
        score.totalScore -= _scoreToDecrease * 2;
        GameManager.Instance.FireScoreUpdateEvent(score.totalScore);
    }
}



[System.Serializable]
public class Score
{
    public int totalScore = 0;
    public Dictionary<ShapeType, int> shapeScoreDic = new Dictionary<ShapeType, int>();
}
