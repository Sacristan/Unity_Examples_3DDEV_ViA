using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinishThingController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreManager.Instance.HandleCoinCollected(); //Inform score manager that coin has been collected
            Destroy(gameObject);
        }
    }

}
