using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float time = 1f;
    [SerializeField]
    private float speed = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(Destroy), time);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Hero"))
        {
            Destroy();
        }
    }
}
