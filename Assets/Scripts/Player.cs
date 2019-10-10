using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    
    [SerializeField]
    private float _speedRotate = 2.0f;
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private float _fireRate = 0.65f;
    private float _canFire = -0.1f;

    [SerializeField]
    private GameObject _firePrefab;
    
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private GameObject[] _engine;

    [SerializeField]
    private AudioClip _audioClip;

    [SerializeField]
    private int _health = 3;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(0,0,0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager not found.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source not found.");
        }
        else
        {
            _audioSource.clip = _audioClip;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");     //Left-Right
        float vertical = Input.GetAxis("Vertical");         //Up-Down
        

        Vector3 direction = new Vector3(0, vertical, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * (horizontal * -1.0f) * _speedRotate);

        if (transform.position.x <= -11.1f)
        {
           transform.position = new Vector3(11.1f, transform.position.y, 0); 
        }
        else if (transform.position.x >= 11.1f)
        {
            transform.position = new Vector3(-11.1f, transform.position.y, 0); 
        }

        if (transform.position.y >= 7.5f)
        {
            transform.position = new Vector3(transform.position.x, -6.7f, 0);
        }
        else if ( transform.position.y <= -6.7)
        {
            transform.position = new Vector3(transform.position.x, 7.5f, 0);
        }
    }

    private void Shoot()
    {
        _canFire = Time.time + _fireRate;

        if (!_isTripleShotActive)
        {
            Instantiate(_firePrefab, transform.position, transform.rotation);
        }
        else 
        {
            Instantiate(_tripleShotPrefab, transform.position, transform.rotation);
        }
        
        _audioSource.Play();
    }

    public void OnDamage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }
        
        _health -= 1;

        _uiManager.UpdateLives(_health);
        
        if (_health == 2)
        {
            _engine[0].SetActive(true);
        }
        else if (_health == 1)
        {
            _engine[1].SetActive(true);
        }

        if (_health <= 0)
        {
            _spawnManager.GameOver();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void OnShieldActive()
    {
        _shield.SetActive(true);
        _isShieldActive = true;
    }

    public void OnSpeedActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(OnSpeedActiveRoutine());
    }

    public void OnTripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(OnTripleShotActiveRoutine());
    }

    IEnumerator OnSpeedActiveRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    IEnumerator OnTripleShotActiveRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
}
