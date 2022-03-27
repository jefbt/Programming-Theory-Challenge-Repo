using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndingPlace"))
        {
            GameManager.PrepareFinalCinematic(
                GetComponent<PlayerShip>(), other.GetComponentInParent<GateControl>());
        }
        else if (other.CompareTag("Collectible"))
        {
            GameManager.GetCollectible(other.gameObject);
        }
        else if (!other.CompareTag("Portal"))
        {
            GameManager.PlayerCrash(gameObject);
        }
    }
}
