using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollect : MonoBehaviour
{
    public int keysCollected = 0;
    public int maxKeys = 5;
    public Text keyCounterText;
        
    void UpdateKeyUI()
    {
        keyCounterText.text = "Собрано " + keysCollected + "/" + maxKeys + " ключей";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            keysCollected++;
            UpdateKeyUI(); 
        }
    }
}
