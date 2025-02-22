using TMPro;
using UnityEngine;

public class EndScreen : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestText;

    public override void SetEnable()
    {
        base.SetEnable();

        SetScore();
        SetBestScore();
    }

    int GetScore()
    {
        return FindObjectOfType<GameManager>().GetScore();
    }

    void SetScore()
    {
        _scoreText.text = GetScore().ToString();
    }

    void SetBestScore()
    {
        int lastScore = GetScore();
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);

        if (lastScore > bestScore)
        {
            _bestText.text = lastScore.ToString();
            PlayerPrefs.SetInt("HighScore", lastScore);
        }
        else
        {
            _bestText.text = bestScore.ToString();
        }
    }
}
