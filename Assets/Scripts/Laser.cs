using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 22.0f;

    // Start is called before the first frame update
    private void Start()
    {   
    }

    // Update is called once per frame
    private void Update()
    { 
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 6.4f || transform.position.y <= -6.4f)
        {
            DestroyLaser();
        }

        if (transform.position.x <= -11.5f || transform.position.x >= 11.1f)
        {
            DestroyLaser();
        }
    }

    private void DestroyLaser()
    {
        if( transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }

        Destroy(this.gameObject);
    }
}
