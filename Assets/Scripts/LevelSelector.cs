using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LevelSelector : MonoBehaviour
{
    private Button LevelSelect;
    private Animator animator;
    [SerializeField] private int levelIndex;
    void Start()
    {
        LevelSelect = GetComponent<Button>();
        animator = GetComponent<Animator>();
        LevelSelect.onClick.AddListener(LoadLevel);
        if (LevelManager.Instance.GetLevelStatus(levelIndex) == LevelStatus.locked)
        {
            LevelSelect.interactable = false;
            animator.enabled = false;
        }
        else
        {
            LevelSelect.interactable = true;
            animator.enabled = true;
        }
    }
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(levelIndex);
    }
}
