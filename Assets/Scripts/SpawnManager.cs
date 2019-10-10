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

    private bool _isGameOver;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPoweupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        
        while (_isGameOver == false)
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

        while (_isGameOver == false)
        {
            Vector3 position = new Vector3(Random.Range(-9f,9f), 6.75f, 0);
            int randomPowerup = Random.Range(0,3);
            GameObject newPowerup = Instantiate(_powerupPrefab[randomPowerup], position, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
