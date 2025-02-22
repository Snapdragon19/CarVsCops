using System;
using TMPro;
using UnityEngine;

public class GameplayMenu : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] private TMP_Text _scoreText;

    public override void SetEnable()
    {
        base.SetEnable();

        GameManager gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        int score = gm.GetScore();
        UpdateScore(score);

        GameManager.OnUpdateScore += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateScore -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
