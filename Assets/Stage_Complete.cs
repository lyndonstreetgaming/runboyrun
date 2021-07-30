using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage_Complete : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI TimeBonusText;

    public TextMeshProUGUI CoinBonusText;
    
    public PlayerController playercontroller;

    private void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    { 
        StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation()
    {
        int Score = PlayerController.Score;

        int TimeBonus = PlayerController.TimeBonusScore;

        int CoinBonus = PlayerController.CoinBonusScore;

        ScoreText.text = Score.ToString();

        TimeBonusText.text = TimeBonus.ToString();

        CoinBonusText.text = CoinBonus.ToString();

        yield return new WaitForSeconds(1f);

        while (PlayerController.TimeBonusScore != 0)
        {
            PlayerController.TimeBonusScore = PlayerController.TimeBonusScore - 10;

            TimeBonusText.text = TimeBonus.ToString();
        }

        while (Score < PlayerController.Score)
        {
            Score = Score + 10;

            ScoreText.text = Score.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
