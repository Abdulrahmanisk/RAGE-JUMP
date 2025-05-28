using UnityEngine;
public class FinalLevelController : MonoBehaviour
{
    [SerializeField] Animator BallinAnimator;
    void Start()
    {
        BallinAnimator.SetTrigger("StartLoop");
    }
    public void LoadMainMenu()
    {
        LevelManager.Instance.LoadMainMenu();
    }
    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
    }
}
