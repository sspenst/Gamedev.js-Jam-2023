using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject powerupPrefab;

    private GameManager gameManager;

    private float xSpawn;
    private float ySpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // set bounds on start (doesn't account for window resize)
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        xSpawn = topRight.x + 0.5f;
        ySpawn = topRight.y + 0.5f;

        StartCoroutine(CreateMissile());
    }

    IEnumerator CreateMissile()
    {
        float sign = Random.Range(0, 2) * 2 - 1;
        Vector3 position = Random.Range(0, 2) == 0 ?
            new Vector3(sign * xSpawn, Random.Range(-ySpawn, ySpawn)) :
            new Vector3(Random.Range(-xSpawn, xSpawn), sign * ySpawn);

        GameObject prefab = Random.Range(0, 10) == 0 && !gameManager.hasPowerup ? powerupPrefab : missilePrefab;

        Instantiate(prefab, position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        if (!gameManager.isGameOver)
        {
            StartCoroutine(CreateMissile());
        }
    }
}
