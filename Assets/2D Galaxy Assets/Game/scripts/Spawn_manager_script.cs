using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_manager_script : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Enemy;
    [SerializeField]
    private GameObject[] PowerUps;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Spawning_enemies());
        StartCoroutine(SpawningPowerUps());
    }
    public void StartSpawnRoutine()
    {
        StartCoroutine(Spawning_enemies());
        StartCoroutine(SpawningPowerUps());
    }

    private IEnumerator Spawning_enemies()
    {
        while(_gameManager.GameOver==false)
        {
            float randomX = Random.Range(-7.95f, 7.95f);
            Instantiate(Enemy[0], new Vector3(randomX, 6.25f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5f);

        }
    }

    private IEnumerator SpawningPowerUps()
    {   
        while(_gameManager.GameOver==false) 
        {

            float randomX = Random.Range(-7.95f, 7.95f);
            int randomNum = Random.Range(0, 4);
            if (randomNum == 3)
            {
                yield return new WaitForSeconds(7f);
                Debug.Log("powerUp wait for 7 sec");
            }
            else
            {
                yield return new WaitForSeconds(7f);
                Instantiate(PowerUps[randomNum], new Vector3(randomX, 6.25f, 0), Quaternion.identity);

            }
        }
         

    }
}
