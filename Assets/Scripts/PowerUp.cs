using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public GameObject vfx;
    public PlayerMovement player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Powerup was picked");
            Pickup();
        }
    }

    private void Pickup()
    {
        //Instantiate(vfx, transform.position, transform.rotation);

        StartCoroutine(SpeedBoost());
        
    }

    private IEnumerator SpeedBoost()
    {
        //gameObject.GetComponent<Collider2D>().enabled = false;
        //gameObject.GetComponent<Renderer>().enabled = false;

        //Debug.Log("Speed Boost applied");
        //player.moveSpeed = 20f;
        yield return new WaitForSeconds(5f);
        //player.moveSpeed = 5f;
        //Debug.Log("Speed Boost ended");
    }

}
