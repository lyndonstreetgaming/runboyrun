using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Changer : MonoBehaviour
{
    public Animator Animator;

    private int LoadLevel;

    public void FadeTo(int sceneindex, LoadSceneMode mode = LoadSceneMode.Single)
    {
        LoadLevel = sceneindex;

        Animator.SetTrigger("FadeOut");
    }

    public void NextLevelFade()
    {
        FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene(LoadLevel);
    }
}
