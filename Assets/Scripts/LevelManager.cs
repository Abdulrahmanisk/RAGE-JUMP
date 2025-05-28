using UnityEngine;
using UnityEngine.SceneManagement;
public enum LevelStatus
{
    locked, unlocked, completed
}
public enum PlayerControl
{
    singlePlayer, twoPlayer
}
public class LevelManager : MonoBehaviour
{
    public PlayerControl playerControl;
    private static LevelManager instance = null;
    public static LevelManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        playerControl = PlayerControl.singlePlayer;
        if (GetLevelStatus(0) == LevelStatus.locked)
            SetLevelStatus(0, LevelStatus.unlocked);
        if (GetLevelStatus(1) == LevelStatus.locked)
            SetLevelStatus(1, LevelStatus.unlocked);
    }
    public void SetLevelComplete()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SetLevelStatus(currentScene, LevelStatus.completed);
        int nextScene = currentScene + 1;
        if (GetLevelStatus(nextScene) == LevelStatus.locked)
            SetLevelStatus(nextScene, LevelStatus.unlocked);
    }
    public void SetLevelStatus(int level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level.ToString(), (int)levelStatus);
    }
    public LevelStatus GetLevelStatus(int level)
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level.ToString(), 0);
        return levelStatus;
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        int index = level;
        switch (GetLevelStatus(index))
        {
            case LevelStatus.locked:
                break;
            case LevelStatus.unlocked:
                AudioManager.Instance.PlaySound(SoundType.ButtonClick);
                AudioManager.Instance.PlaySound(SoundType.BGMusic);
                SceneManager.LoadScene(index);
                break;
            case LevelStatus.completed:
                AudioManager.Instance.PlaySound(SoundType.ButtonClick);
                AudioManager.Instance.PlaySound(SoundType.BGMusic);
                SceneManager.LoadScene(index);
                break;
        }
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PlaySound(SoundType.BGMusic);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.PlaySound(SoundType.BGMusic);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void QuitGame()
    {
        Application.Quit();
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void SwitchPlayerControl()
    {
        if (playerControl == PlayerControl.singlePlayer)
            playerControl = PlayerControl.twoPlayer;
        else
            playerControl = PlayerControl.singlePlayer;
    }
}
