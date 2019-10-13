using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerupPrefab;

    [SerializeField]
    private GameObject _powerupContainer;

    [SerializeField]
    private GameObject _asteroidPrefab;

    [SerializeField]
    private GameObject _asteroidContainer;

    private bool _isGameOver = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        
        while (!_isGameOver)
        {
            Vector3 position = new Vector3(Random.Range(-9f,9f), 6.75f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPoweupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!_isGameOver)
        {
            Vector3 position = GetRandomPosition();            
            int randomPowerup = Random.Range(0,3);
            GameObject newPowerup = Instantiate(_powerupPrefab[randomPowerup], position, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    IEnumerator SpawnAsteroidRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        while(!_isGameOver)
        {
            Vector3 position = GetRandomPosition();
            GameObject newAsteroid = Instantiate(_asteroidPrefab, position, Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;
            Asteroid asteroid = newAsteroid.GetComponent<Asteroid>();
            asteroid.AssignExtra();
            yield return new WaitForSeconds(10);
        }
    }

    private Vector3 GetRandomPosition()
    {
        int spawnLocation = Random.Range(0,4);
        switch(spawnLocation)
        {
            case 0:  // top
                return new Vector3(Random.Range(-9f,9f), 6.75f, 0);
            case 1: // bottom
                return new Vector3(Random.Range(-9f,9f), -6.75f, 0);
            case 2: // left
                return new Vector3(-11f, Random.Range(4.5f,-4.5f), 0);
            case 3: // right
                return new Vector3(11f, Random.Range(4.5f,-4.5f), 0);
            default:
                return Vector3.zero;
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPoweupRoutine());
        StartCoroutine(SpawnAsteroidRoutine());
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void OnGamePause()
    {
        StopAllCoroutines();
    }

    public void OnGameUnpause()
    {
        StartSpawning();
    }
}
