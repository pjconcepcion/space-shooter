﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private AudioClip _audioClip;

    private Player _player;
    private Animator _animator;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player not found.");
        }

        if (_animator == null)
        {
            Debug.LogError("Animator not found.");
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
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.7f)
        {
            transform.position = new Vector3(Random.Range(-9f,9f), 6.75f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player.OnDamage();

            int randomScore = Random.Range(10, 20);
            _uiManager.UpdateScore(randomScore);

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        if (other.gameObject.tag == "Laser")
        {
            if( other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            int randomScore = Random.Range(10, 20);
            _uiManager.UpdateScore(randomScore);
            
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;

            _audioSource.Play();

            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}