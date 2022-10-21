using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    
    [SerializeField] 
    private float speed = 1f;
    [SerializeField] 
    private GameObject bullet;
    [SerializeField] 
    private float muzzle = 0f;
    [SerializeField] 
    private float waitTime = 1f;
    private float timer = 0f;
    [SerializeField] 
    private float restartTime = 1f;
    
    [SerializeField] 
    private Vector3 startPosition;
    [SerializeField] 
    private float leftLimit;
    [SerializeField] 
    private float rightLimit;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            var shift = speed * Time.deltaTime;
            if (transform.position.x + shift < rightLimit)
            {
                transform.Translate(shift, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            var shift = speed * Time.deltaTime;
            if (transform.position.x - shift > leftLimit)
            {
                transform.Translate(-shift, 0, 0);
            }
        }

        timer += Time.deltaTime;
        if (timer > waitTime && Input.GetKey(KeyCode.Space))
        {
            timer = 0f;
            Instantiate(bullet, transform.position + new Vector3(0, muzzle, 0), Quaternion.identity);
        }
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            var obj = GameObject.FindGameObjectWithTag("Game");
            obj.GetComponent<Game>().UpdateLives();
            
            gameObject.SetActive(false);
            Invoke(nameof(Restart), restartTime);
        }
    }

    private void Restart()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
}
