using UnityEngine;
public enum BallType
{
    BlueBall, RedBall
}
public enum GameOverConditions
{
    BlueInLava, RedInWater, FellIntoAcid
}
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController[] players;
    [SerializeField] CanvasController canvasController;
    [SerializeField] GameObject BlueFinishLight;
    [SerializeField] GameObject RedFinishLight;
    BallType currentBall;
    bool noSwitching = false;
    bool BlueFinished = false;
    bool RedFinished = false;
    int coins;
    void Start()
    {
        if (LevelManager.Instance.playerControl == PlayerControl.singlePlayer)
        {
            PlayerSelect(BallType.BlueBall);
        }
        coins = 0;
        SetCoinCount();
    }
    void DisableAllPlayers()
    {
        foreach (var item in players)
        {
            item.LightDisable();
            item.enabled = false;
        }
    }
    void PlayerSelect(BallType ballType)
    {
        if (!noSwitching)
        {
            DisableAllPlayers();
            foreach (var item in players)
            {
                if (item.ballType == ballType)
                {
                    if (item.enabled == false)
                    {
                        item.enabled = true;
                        item.LightEnable();
                        AudioManager.Instance.PlaySound(SoundType.PlayerSwitch);
                        currentBall = ballType;
                    }
                }
            }
        }
    }
    public void PickedCoinUp()
    {
        AudioManager.Instance.PlaySound(SoundType.Coin);
        coins++;
        SetCoinCount();
    }
    void SetCoinCount()
    {
        canvasController.SetCoinCount(coins);
    }
    public void PlayerFinished(BallType ballType)
    {
        if (ballType == BallType.BlueBall)
        {
            BlueFinished = true;
            BlueFinishLight.SetActive(false);
        }
        if (ballType == BallType.RedBall)
        {
            RedFinished = true;
            RedFinishLight.SetActive(false);
        }
        if (BlueFinished && RedFinished)
        {
            FinishLevel();
            AudioManager.Instance.PlaySound(SoundType.FinishLevel);
            return;
        }
        if (LevelManager.Instance.playerControl == PlayerControl.singlePlayer)
        {
            if (BlueFinished)
            {
                PlayerSelect(BallType.RedBall);
                noSwitching = true;
            }
            else
            {
                PlayerSelect(BallType.BlueBall);
                noSwitching = true;
            }
        }
    }
    [ContextMenu("Switch Player")]
    void SwitchPlayer()
    {
        if (!noSwitching)
        {
            if (currentBall == BallType.BlueBall)
                PlayerSelect(BallType.RedBall);
            else
                PlayerSelect(BallType.BlueBall);
        }
    }
    void Update()
    {
        if (LevelManager.Instance.playerControl == PlayerControl.singlePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchPlayer();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        canvasController.EnablePause();
        AudioManager.Instance.PauseMusic();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        canvasController.DisablePause();
        AudioManager.Instance.ResumeMusic();
    }
    public void GameOver(GameOverConditions gameOverConditions)
    {
        string gameOverReason = GetReason(gameOverConditions);
        Time.timeScale = 0;
        canvasController.GameOverEnable(gameOverReason);
        AudioManager.Instance.PlaySound(SoundType.GameOver);
    }
    public void FinishLevel()
    {
        LevelManager.Instance.SetLevelComplete();
        Time.timeScale = 0;
        canvasController.FinishLevelEnable();
        AudioManager.Instance.PlaySound(SoundType.FinishLevel);
    }
    string GetReason(GameOverConditions gameOverConditions)
    {
        string reason = "";
        switch (gameOverConditions)
        {
            case GameOverConditions.BlueInLava: reason = "Aqua can't swim in lava!"; break;
            case GameOverConditions.RedInWater: reason = "Flamo can't swim in water!"; break;
            case GameOverConditions.FellIntoAcid: reason = "Do not fall into spikes!"; break;
        }
        return reason;
    }
}
