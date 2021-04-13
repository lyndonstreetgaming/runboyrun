using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    private string[] password = new string[] { "aqhgzjg", "whmkhtf", "tntdysg", "qambdxv", "krqynvz", "wwczagk" };

    public TMP_InputField UserInput;

    public string userinput;

    public GameObject PasswordIncorerctPrompt;

    public void PasswordChecker()
    {
        userinput = UserInput.text;

        if (userinput == password[0])
        {
            SceneManager.LoadScene("Level 1 - Stage 1");
        }

        else if (userinput == password[1])
        {
            SceneManager.LoadScene("Level 2 - Stage 1");
        }

        else if (userinput == password[2])
        {
            SceneManager.LoadScene("Level 3 - Stage 1");
        }

        else if (userinput == password[3])
        {
            SceneManager.LoadScene("Level 4 - Stage 1");
        }

        else if (userinput == password[4])
        {
            SceneManager.LoadScene("Level 5 - Stage 1");
        }

        else if (userinput == password[5])
        {
            SceneManager.LoadScene("Level 6 - Stage 1");
        }

        else
        {
            PasswordIncorerctPrompt.SetActive(true);
        }
    }        
}
