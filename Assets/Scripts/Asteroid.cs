using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.3f;
    [SerializeField]
    private float _speedRotation = 5.0f;
    
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    private bool _isExtra = false;
    private Vector3 _movement;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }

        SetMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        if(_isExtra)
        {
            transform.Translate(_movement * _speed * Time.deltaTime);
            
            if (transform.position.y <= -6.75f || transform.position.y >= 6.75f || transform.position.x >= 11f || transform.position.x <= -11f)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser" && !_isExtra)
        {
            OnExplode();
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
        }

        if (other.gameObject.tag == "Laser" && _isExtra)
        {            
            OnExplode();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Player" && _isExtra)
        {
            Player player = other.GetComponent<Player>();

            if (player == null)
            {
                Debug.LogError("Player not found.");
            }

            OnExplode();
            player.OnDamage();
        }
    }

    private void OnExplode()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void SetMovement()
    {
        if (transform.position.y == 6.75f)
        {
            _movement = Vector3.down;
        }
        else if (transform.position.y == -6.75f)
        {
            _movement = Vector3.up;
        }

        if (transform.position.x == 11f)
        {
            _movement = Vector3.left;
        }
        else if (transform.position.x == -11f)
        {
            _movement = Vector3.right;
        }
    }

    public void AssignExtra()
    {
        _isExtra = true;
        transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }

    public void OnGamePause()
    {
        _speed = 0;
        _speedRotation = 0;
    }

    public void OnGameUnpause()
    {
        _speed = 0.3f;
        _speedRotation = 5.0f;
    }
}
