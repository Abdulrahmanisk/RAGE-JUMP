using UnityEngine;
using UnityEngine.Rendering.Universal;
public class SwitchController : MonoBehaviour
{
    [SerializeField] Light2D lightComponent;
    [SerializeField] PlatformController[] movingPlatforms;
    bool switchOn = false;
    void Start()
    {
        lightComponent.color = Color.white;
        switchOn = false;
    }
    void EnableMovement()
    {
        foreach (var item in movingPlatforms)
        {
            item.MovePlatform();
        }
    }
    public void TriggerSwitch(BallType ballType)
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (!switchOn)
        {
            if (ballType == BallType.BlueBall)
            {
                lightComponent.color = Color.blue;
            }
            else if (ballType == BallType.RedBall)
            {
                lightComponent.color = Color.red;

            }
            AudioManager.Instance.PlaySound(SoundType.Machine);
            EnableMovement();
            switchOn = !switchOn;
        }
    }
}
