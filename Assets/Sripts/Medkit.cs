using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] public int healAmount = 25;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
