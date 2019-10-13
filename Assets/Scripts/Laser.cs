using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 22.0f;

    private bool _isEnemyLaser = false;
    
    // Start is called before the first frame update
    private void Start()
    {             
    }

    // Update is called once per frame
    private void Update()
    {        
        Move();
        
        if (transform.position.y >= 6.4f || transform.position.y <= -6.4f)
        {
            DestroyLaser();
        }

        if (transform.position.x <= -11.5f || transform.position.x >= 11.1f)
        {
            DestroyLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && _isEnemyLaser)
        {            
            Player _player = GameObject.Find("Player").GetComponent<Player>();

            if (_player == null)
            {
                Debug.LogError("Player not found.");
            }      

            DestroyLaser();
            _player.OnDamage();
        }

        if (other.gameObject.tag == "Enemy" && !_isEnemyLaser)
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            
            if (_enemy == null)
            {
                Debug.LogError("Enemy not found.");
            }

            DestroyLaser();
            _enemy.OnEnemyDeath();
        }
    }

    private void Move()
    {        
        if (_isEnemyLaser)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else{
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
    }

    private void DestroyLaser()
    {
        Debug.Log(transform.parent.name);
        if( transform.parent.tag == "TripleShot" && transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }

        Destroy(this.gameObject);
    }

    public void AssignEnemy()
    {   
        _isEnemyLaser = true;
    }

    public void OnGamePause()
    {
        _speed = 0;
    }

    public void OnGameUnpause()
    {
        _speed = 22.0f;
    }
}
