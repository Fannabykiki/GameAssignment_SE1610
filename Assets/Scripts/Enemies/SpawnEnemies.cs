using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    private float timeToSpawn = 3;
    public List<GameObject> ene1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy1());
        StartCoroutine(SpawnEnemy2());
        StartCoroutine(SpawnEnemy3());
    }

    private IEnumerator SpawnEnemy1()
    {
        while (true)
        {
            var check = Random.Range(1, 5);
            Debug.Log($"random 1 : {check}");
            if (ene1[0] != null)
            {
                if (check == 1)
                {
                    ene1[0].transform.position = new Vector3(-23.33f, Random.Range(20.29f, 18.84f), 0);
                }
                else if (check == 2)
                {
                    ene1[0].transform.position = new Vector3(-20.28f, Random.Range(-9.3f, -12.4f), 0);
                }
                else if (check == 3)
                {
                    ene1[0].transform.position = new Vector3(Random.Range(36.98f, 34f), -9.5f, 0);
                }
                else
                {
                    ene1[0].transform.position = new Vector3(28.1f, 18f, 0);
                }
                ene1[0].SetActive(true);
            }
            yield return new WaitForSeconds(timeToSpawn);

        }

    }
    private IEnumerator SpawnEnemy2()
    {
        while (true)
        {
            var check = Random.Range(1, 5);
            Debug.Log($"random 2 : {check}");

            if (ene1[2] != null)
            {
                if (check == 1)
                {
                    ene1[2].transform.position = new Vector3(-23.33f, Random.Range(20.29f, 18.84f), 0);
                }
                else if (check == 2)
                {
                    ene1[2].transform.position = new Vector3(-20.28f, Random.Range(-9.3f, -12.4f), 0);
                }
                else if (check == 3)
                {
                    ene1[2].transform.position = new Vector3(Random.Range(36.98f, 34f), -9.5f, 0);
                }
                else
                {
                    ene1[2].transform.position = new Vector3(28.1f, 18f, 0);
                }
                ene1[2].SetActive(true);
            }
            yield return new WaitForSeconds(timeToSpawn);

        }

    }
    private IEnumerator SpawnEnemy3()
    {
        while (true)
        {
            var check = Random.Range(1, 5);
            Debug.Log($"random 3 : {check}");
            if (ene1[1] != null)
            {
                if (check == 1)
                {
                    ene1[1].transform.position = new Vector3(-23.33f, Random.Range(20.29f, 18.84f), 0);
                }
                else if (check == 2)
                {
                    ene1[1].transform.position = new Vector3(-20.28f, Random.Range(-9.3f, -12.4f), 0);
                }
                else if (check == 3)
                {
                    ene1[1].transform.position = new Vector3(Random.Range(36.98f, 34f), -9.5f, 0);
                }
                else
                {
                    ene1[1].transform.position = new Vector3(28.1f, 18f, 0);
                }
                ene1[1].SetActive(true);
            }
            yield return new WaitForSeconds(timeToSpawn);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
