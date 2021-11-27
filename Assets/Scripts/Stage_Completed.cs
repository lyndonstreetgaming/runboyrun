using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_Completed : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI TimeBonusText;

    public TextMeshProUGUI CoinBonusText;

    public TextMeshProUGUI PlayerUIScoreText;

    public PlayerController playercontroller;

    private void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        playercontroller.TimeBonuses();

        playercontroller.CoinBonuses();

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int Score = PlayerController.Score;

        ScoreText.text = Score.ToString();

        PlayerUIScoreText.text = Score.ToString();

        int TimeBonus = PlayerController.TimeBonus;

        TimeBonusText.text = TimeBonus.ToString();

        int CoinBonus = PlayerController.CoinBonus;

        CoinBonusText.text = CoinBonus.ToString();

        yield return new WaitForSeconds(5f);

        PlayerController.Score += PlayerController.TimeBonus + PlayerController.CoinBonus;

        while (CoinBonus != 0 || TimeBonus != 0 || Score < PlayerController.Score)
        {
            if (TimeBonus != 0) TimeBonus -= 100;

            if (CoinBonus != 0) CoinBonus -= 100;

            if (Score < PlayerController.Score)
            {
                Score += 100;
            }
            TimeBonusText.text = TimeBonus.ToString();

            CoinBonusText.text = CoinBonus.ToString();

            ScoreText.text = Score.ToString();

            PlayerUIScoreText.text = Score.ToString();

            yield return new WaitForSeconds(.03f);
        }
    }
}

