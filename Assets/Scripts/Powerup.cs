using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    protected GameManager gameManager;

    protected float distance;

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
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            
            StartCoroutine(UsePowerup());
        }
    }

    protected IEnumerator UsePowerup()
    {
        player.GetComponent<PlayerController>().shield.SetActive(true);
        gameManager.hasPowerup = true;

        yield return new WaitForSeconds(5);

        // back to normal
        gameManager.hasPowerup = false;
        player.GetComponent<PlayerController>().shield.SetActive(false);
        Destroy(gameObject);
    }
}
