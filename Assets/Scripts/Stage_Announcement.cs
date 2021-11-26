using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Announcement : MonoBehaviour
{
    public GameObject Announcement_UI;

    public GameObject Player_UI;

    private void Start()
    {
        Disabled();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Enabled();
        }
    }

    public void Enabled()
    {
        StartCoroutine("EnabledDelay");
    }


    public void Disabled()
    {

        Announcement_UI.SetActive(true);

        Player_UI.SetActive(false);

        Time.timeScale = 0f;
    }

    public IEnumerator EnabledDelay()
    {
        float PauseTime = Time.realtimeSinceStartup + 1f;

        while (Time.realtimeSinceStartup < PauseTime)
        {
            yield return 0;
        }

        Announcement_UI.SetActive(false);

        Player_UI.SetActive(true);

        Time.timeScale = 1f;

    }
}
