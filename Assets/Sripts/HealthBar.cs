using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Image redBar;
    [SerializeField] public Image yellowBar;
    public float yellowBarDelay = 0.5f; 
    public float yellowBarSpeed = 1f; 

    private float targetHealth; 
    private float yellowBarTimer; 

    void Update()
    {
        // Обновляем красную полоску
        redBar.fillAmount = Mathf.Lerp(redBar.fillAmount, targetHealth, Time.deltaTime * 10f);
        
        if (yellowBarTimer > 0)
        {
            yellowBarTimer -= Time.deltaTime;
        }
        else
        {
            // Плавно уменьшаем желтую полоску
            yellowBar.fillAmount = Mathf.Lerp(yellowBar.fillAmount, targetHealth, Time.deltaTime * yellowBarSpeed);
        }
    }

    public void SetHealth(float healthNormalized)
    {
        // Устанавливаем целевое значение здоровья (от 0 до 1)
        targetHealth = Mathf.Clamp01(healthNormalized);
        yellowBarTimer = yellowBarDelay;
    }
}