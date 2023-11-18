using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private const string PLAYER_PREFS_IS_TUTORIAL_PLAYED = "isTutorialPlayed";
    private int isTutorialPlayed;

    private const int MAIN_MENU_SCENE = 0;
    private const int NEW_TOWN_SCENE = 1;
    private const int TUTORIAL_SCENE = 2;

    private void Start()
    {
        isTutorialPlayed = PlayerPrefs.GetInt(PLAYER_PREFS_IS_TUTORIAL_PLAYED, 0);

        if (isTutorialPlayed == 0 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            DataPersistenceManager.Instance.NewGame();

            SceneManager.LoadSceneAsync(TUTORIAL_SCENE);
        }
    }

    public void OnNewGameClicked()
    {
        DataPersistenceManager.Instance.NewGame();

        SceneManager.LoadSceneAsync(NEW_TOWN_SCENE);
    }

    public void OnLoadGameClicked()
    {
        SceneManager.LoadSceneAsync(NEW_TOWN_SCENE);
    }

    public void OnSaveGameClicked()
    {
        DataPersistenceManager.Instance.SaveGame();

        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void FinishTutorial()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_IS_TUTORIAL_PLAYED, 1);

        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void Exit()
    {
        Application.Quit();
    }
}