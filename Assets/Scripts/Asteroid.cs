using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            
            _spawnManager.StartSpawning();

            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
