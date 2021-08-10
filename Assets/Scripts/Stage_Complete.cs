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

        yield return new WaitForSeconds(2f);

        while (TimeBonus != 0 && CoinBonus != 0)
        {
            TimeBonus = TimeBonus - 10;

            CoinBonus = CoinBonus - 10;

            TimeBonusText.text = TimeBonus.ToString();

            CoinBonusText.text = CoinBonus.ToString();

            PlayerController.Score = PlayerController.Score + PlayerController.TimeBonusScore + PlayerController.CoinBonusScore;
        }

        while (Score < PlayerController.Score)
        {
            yield return new WaitForSeconds(1f);

            Score = Score + 10;

            ScoreText.text = Score.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
