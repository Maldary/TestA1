using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] public int maxHealth = 100;
    public int currentHealth;
    public HealthBar _healthBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);//чтобы платформа могла передвигать персонажа
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            isGrounded = false;
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Здоровье: " + currentHealth);
        UpdateHealthUI();
    }
    
    void UpdateHealthUI()
    {
        float healthNormalized = (float)currentHealth / maxHealth; 
        _healthBar.SetHealth(healthNormalized); 
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Здоровье: " + currentHealth);
        UpdateHealthUI();
    }
    
    

    void Die()
    {
        {
            Debug.Log("Игрок умер! Перезагрузка сцены...");
            StartCoroutine(ReloadSceneWithDelay(2f)); 
        }

        IEnumerator ReloadSceneWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    
}