using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material powerupMaterial;
    public TextMeshProUGUI scoreText;

    [SerializeField] private float speed;
    [SerializeField] private float jumpVelocity;

    private GameManager gameManager;

    private float xBound;
    private float yBound;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // set bounds on start (doesn't account for window resize)
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        xBound = topRight.x - 0.5f;
        yBound = topRight.y - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            transform.position = new Vector3(
                transform.position.x + horizontalInput * Time.deltaTime * speed,
                transform.position.y + verticalInput * Time.deltaTime * speed,
                transform.position.z
            );

            CheckBounds();
        }
    }

    void CheckBounds()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
        else if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
    }
}
