using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Clone : MonoBehaviour
{

    [SerializeField] float speed = 3f;
    [SerializeField] GameObject laserPrefab;
    Vector3 direction=Vector3.left;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    Coroutine firingCoroutine;

    private void Start()
    {
        StartCoroutine(die());
        StartCoroutine(shoot());
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x <= -4.5f)
        {
            direction = Vector3.right;
        }

        else if (transform.position.x >= 4.5f)
        {
            direction = Vector3.left;
        }
    }

   IEnumerator shoot()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
        
}
