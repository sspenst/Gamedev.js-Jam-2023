using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Material detonateMaterial;

    private GameObject player;
    private Rigidbody rb;
    protected GameManager gameManager;

    protected float distance;
    private bool isDetonated = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            transform.LookAt(player.transform);
            transform.Rotate(Vector3.right * 90);
            transform.Rotate(Vector3.up * 90);

            distance = (player.transform.position - transform.position).magnitude;

            float speed = -10 * Mathf.Log10((distance + 1) / 10);
            float minSpeed = 2;
            float maxSpeed = 5;

            speed = Mathf.Max(Mathf.Min(speed, maxSpeed), minSpeed);

            rb.velocity = transform.rotation * Vector3.up * speed;

            float detonateRadius = 4;

            if (!isDetonated && distance < detonateRadius)
            {
                isDetonated = true;
                StartCoroutine(DetonateCoroutine());
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator DetonateCoroutine()
    {
        GetComponent<Renderer>().material = detonateMaterial;

        yield return new WaitForSeconds(2);

        Detonate();
    }

    void Detonate()
    {
        if (!gameManager.isGameOver)
        {
            Destroy(gameObject);
            gameManager.AddScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager.hasPowerup)
            {
                Detonate();
            }
            else
            {
                gameManager.GameOver();
            }
        }
    }
}
