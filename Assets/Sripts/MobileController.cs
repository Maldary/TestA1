using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MobileController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] public int maxHealth = 100;
    public int currentHealth;
    public HealthBar _healthBar;
    private bool isMoving = false;
    private int moveDirection = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        HandleTouchInput();
        MovePlayer();
    }
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }
            else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    moveDirection = 1;
                    isMoving = true;
                }
                else
                {
                    moveDirection = -1;
                    isMoving = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
                moveDirection = 0;
            }
        }
    }
    void MovePlayer()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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