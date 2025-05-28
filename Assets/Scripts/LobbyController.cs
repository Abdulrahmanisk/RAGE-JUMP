using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text soundText;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private Image onePlayer;
    [SerializeField] private Image twoPlayer;
    private
    float musicVol;
    float effectsVol;
    void Start()
    {
        if (LevelManager.Instance.playerControl == PlayerControl.singlePlayer)
        {
            onePlayer.enabled = true;
            twoPlayer.enabled = false;
        }
        else
        {
            onePlayer.enabled = false;
            twoPlayer.enabled = true;
        }
        musicVol = AudioManager.Instance.GetMusicVolume() * 10;
        effectsVol = AudioManager.Instance.GetSfxVolume() * 10;
        UpdateEffectsVolText();
        UpdateMusicVolText();
        StartCoroutine(TitleLoopAnim());
    }
    public void StartGame()
    {
        animator.SetTrigger("LevelSelectOn");
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void OpenOptions()
    {
        animator.SetTrigger("SettingsOn");
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void SettingsBackToMenu()
    {
        animator.SetTrigger("SettingsOff");
        AudioManager.Instance.PlaySound(SoundType.BackButton);
    }
    public void LevelSelectBackToMenu()
    {
        animator.SetTrigger("LevelSelectOff");
        AudioManager.Instance.PlaySound(SoundType.BackButton);
    }
    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void SetSfxVolume(float _volume)
    {
        AudioManager.Instance.SetSfxVolume(_volume);
    }
    public void SetMusicVolume(float _volume)
    {
        AudioManager.Instance.SetMusicVolume(_volume);
    }
    void UpdateMusicVolText()
    {
        musicText.text = musicVol.ToString();
    }
    void UpdateEffectsVolText()
    {
        soundText.text = effectsVol.ToString();
    }
    public void IncreaseMusic()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (musicVol < 10)
            musicVol++;
        SetMusicVolume(musicVol / 10);
        UpdateMusicVolText();
    }
    public void DecreaseMusic()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (musicVol > 0)
            musicVol--;
        SetMusicVolume(musicVol / 10);
        UpdateMusicVolText();
    }
    public void IncreaseEffects()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (effectsVol < 10)
            effectsVol++;
        SetSfxVolume(effectsVol / 10);
        UpdateEffectsVolText();
    }
    public void DecreaseEffects()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (effectsVol > 0)
            effectsVol--;
        SetSfxVolume(effectsVol / 10);
        UpdateEffectsVolText();
    }
    IEnumerator TitleLoopAnim()
    {
        yield return new WaitForSeconds(2.5f);
        titleAnimator.SetTrigger("StartLoop");
        AudioManager.Instance.PlaySound(SoundType.MenuMusic);
    }
    public void SelectOnePlayer()
    {
        if (LevelManager.Instance.playerControl == PlayerControl.twoPlayer)
        {
            LevelManager.Instance.SwitchPlayerControl();
            onePlayer.enabled = true;
            twoPlayer.enabled = false;
        }
    }
    public void SelectTwoPlayer()
    {
        if (LevelManager.Instance.playerControl == PlayerControl.singlePlayer)
        {
            LevelManager.Instance.SwitchPlayerControl();
            onePlayer.enabled = false;
            twoPlayer.enabled = true;
        }
    }
}
