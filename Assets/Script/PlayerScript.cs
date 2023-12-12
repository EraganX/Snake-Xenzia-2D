using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    private static int speed;
    private Vector2 direction;

    [Header("Grow")]
    private List<Transform> _snakeSegmentList;
    [SerializeField] private Transform _segmentPrefab;

    public int countFoodEat = 0;

    [Header("Bound")]
    public BoxCollider2D playGround;


    FoodManager foodManager;
    ScoreManager scoreManager;

    void Awake()
    {
        foodManager = FindObjectOfType<FoodManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        _snakeSegmentList = new List<Transform>();
        _snakeSegmentList.Add(this.transform);
    }

    void Start()
    {
        direction = Vector2.right;
    }

    void Update()
    {
        UserInput();
        PlayerInBound();

    }

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction.y != -1)
        {
            direction = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && direction.y != 1)
        {
            direction = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction.x != 1)
        {
            direction = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction.x != -1)
        {
            direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(
            direction.x * speed,
            direction.y * speed,
             0) * Time.fixedDeltaTime;

        for (int i = _snakeSegmentList.Count - 1; i > 0; i--)
        {
            _snakeSegmentList[i].position = _snakeSegmentList[i - 1].position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            scoreManager.IncreaseScore(speed - 7);
            countFoodEat++;
            Grow();
        }

        if (other.CompareTag("Bonus"))
        {
            scoreManager.IncreaseScore((Mathf.RoundToInt((speed - 7) * 10 * foodManager.bonusValue) * (speed - 7)));
            Destroy(other.gameObject);
        }
    }

    public static void SetSpeed(int changeSpeed)
    {
        switch (changeSpeed)
        {
            case 0:
                speed = 12; break;
            case 1:
                speed = 13; break;
            case 2:
                speed = 14; break;
            case 3:
                speed = 15; break;
            case 4:
                speed = 16; break;
            case 5:
                speed = 17; break;
            case 6:
                speed = 18; break;
            case 7:
                speed = 19; break;
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _snakeSegmentList[_snakeSegmentList.Count - 1].position;
        _snakeSegmentList.Add(segment);
    }

    private void PlayerInBound()
    {
        Bounds bound = playGround.bounds;

        if (transform.position.x > bound.max.x)
        {
            transform.position = new Vector3(bound.min.x + 0.1f, transform.position.y, 0);
        }
        if (transform.position.x < bound.min.x)
        {
            transform.position = new Vector3(bound.max.x - 0.1f, transform.position.y, 0);
        }
        if (transform.position.y > bound.max.y)
        {
            transform.position = new Vector3(transform.position.x, bound.min.y + 0.1f, 0);
        }
        if (transform.position.y < bound.min.y)
        {
            transform.position = new Vector3(transform.position.x, bound.max.y - 0.1f, 0);
        }
    }

}
