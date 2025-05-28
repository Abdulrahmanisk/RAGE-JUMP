using UnityEngine;
using TMPro;
public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject PauseGamePanel;
    [SerializeField] GameObject FinishLevelPanel;
    [SerializeField] TMP_Text gameOverReason;
    [SerializeField] TMP_Text coinCount;
    public void SetCoinCount(int count)
    {
        coinCount.text = count.ToString();
    }
    public void EnablePause()
    {
        Time.timeScale = 0;
        PauseGamePanel.SetActive(true);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void DisablePause()
    {
        if (PauseGamePanel.activeInHierarchy)
            PauseGamePanel.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.PlaySound(SoundType.BackButton);
    }
    public void GameOverEnable(string reason)
    {
        gameOverReason.text = reason;
        GameOverPanel.SetActive(true);
    }
    public void FinishLevelEnable()
    {
        FinishLevelPanel.SetActive(true);
    }
    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }
    public void MainMenu()
    {
        LevelManager.Instance.LoadMainMenu();
    }
    public void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
