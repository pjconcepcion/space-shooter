using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private float _powerupId;

    [SerializeField]
    private AudioClip _audioClip;

    private Player _player;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player not found.");
        }   
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.75f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch(_powerupId)
            {
                case 0: 
                    _player.OnShieldActive();
                    break;
                case 1:
                    _player.OnSpeedActive();
                    break;
                case 2:
                    _player.OnTripleShotActive();
                    break;
                default:
                    Debug.Log("Default powerup");
                    break;
            }

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Destroy(this.gameObject);
        }
    }
}
