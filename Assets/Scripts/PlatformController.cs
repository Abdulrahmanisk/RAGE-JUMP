using UnityEngine;
public enum Direction
{
    up, down, left, right
}
public class PlatformController : MonoBehaviour
{
    bool movePlatform = false;
    [SerializeField] Direction direction;
    [SerializeField] float distance;
    Vector2 directionVector;
    Vector2 currentPos;
    Vector2 targetPos;
    void Start()
    {
        switch (direction)
        {
            case Direction.up:
                directionVector = new Vector2(0, 1);
                break;
            case Direction.down:
                directionVector = new Vector2(0, -1);
                break;
            case Direction.left:
                directionVector = new Vector2(-1, 0);
                break;
            case Direction.right:
                directionVector = new Vector2(1, 0);
                break;
        }
        currentPos = transform.position;
        targetPos = currentPos + directionVector * distance;
    }
    public void MovePlatform()
    {
        movePlatform = true;
    }
    void FixedUpdate()
    {
        if (movePlatform)
        {
            currentPos = currentPos + directionVector * 0.05f;
            transform.position = currentPos;
            if (Mathf.Abs(transform.position.x) > Mathf.Abs(targetPos.x) || Mathf.Abs(transform.position.y) > Mathf.Abs(targetPos.y))
            {
                movePlatform = false;
            }
        }
    }
}
