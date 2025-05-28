using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 22f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public BallType ballType;
    [SerializeField] private Light2D lightComponent;
    [SerializeField] private GameManager gameManager;
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        if (LevelManager.Instance.playerControl == PlayerControl.twoPlayer)
        {
            if (ballType == BallType.BlueBall)
            {
                if (Input.GetKey(KeyCode.A))
                    horizontal = -1f;
                else if (Input.GetKey(KeyCode.D))
                    horizontal = 1f;
                else
                    horizontal = 0f;

                if (Input.GetKeyDown(KeyCode.W))
                    vertical = 1f;
                else
                    vertical = 0f;
            }
            else if (ballType == BallType.RedBall)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    horizontal = -1f;
                else if (Input.GetKey(KeyCode.RightArrow))
                    horizontal = 1f;
                else
                    horizontal = 0f;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    vertical = 1f;
                else
                    vertical = 0f;
            }
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        if (vertical == 1f && isGrounded)
        {
            AudioManager.Instance.PlaySound(SoundType.Jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    public void LightDisable()
    {
        lightComponent.enabled = false;
    }
    public void LightEnable()
    {
        lightComponent.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
        {
            gameManager.PickedCoinUp();
            Destroy(col.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            if (ballType == BallType.BlueBall && col.gameObject.tag == "BlueFinish")
            {
                gameManager.PlayerFinished(ballType);
                gameObject.SetActive(false);
            }
            else if (ballType == BallType.RedBall && col.gameObject.tag == "RedFinish")
            {
                gameManager.PlayerFinished(ballType);
                gameObject.SetActive(false);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Switch"))
        {
            col.GetComponent<SwitchController>().TriggerSwitch(ballType);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Acid"))
        {
            gameManager.GameOver(GameOverConditions.FellIntoAcid);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            if (ballType == BallType.RedBall)
            {
                speed = speed / 2;
            }
            else
            {
                gameManager.GameOver(GameOverConditions.BlueInLava);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (ballType == BallType.BlueBall)
            {
                speed = speed / 2;
            }
            else
            {
                gameManager.GameOver(GameOverConditions.RedInWater);
            }
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            if (ballType == BallType.RedBall)
            {
                speed = speed * 2;
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (ballType == BallType.BlueBall)
            {
                speed = speed * 2;
            }
        }
    }
}
