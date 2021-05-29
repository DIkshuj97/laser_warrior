using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int powerupID;

    [SerializeField] private float speed = 3.0f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        if(othercollider.tag=="Player")
        {
            Player player = othercollider.GetComponent<Player>();
            if (powerupID == 0)
            {
                player.TripleShotPowerupOn();
            }

            else if (powerupID==1)
            {
                player.EnableShields();
            }

            else if (powerupID == 2)
            {
                player.healthpickup();
            }

            else if (powerupID == 3)
            {
                player.EnablePartner();
            }

            Destroy(gameObject);
        }
    }
}
