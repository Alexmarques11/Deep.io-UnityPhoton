using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public PhotonView view;

    public string playerName;
    public TMP_Text nicknameText;
    private SpriteRenderer spriteRenderer;

    static public PlayerController localPlayer;

    [Photon.Pun.PunRPC]
    public string nickname = "";

    [PunRPC]
    public Color playerColor;

    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    public int score = 0;
    public TMP_Text scoreText;

    void Start()
    {
        view = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();

        currentHealth = maxHealth;

        if (view.IsMine)
        {
            Camera.main.GetComponent<CameraController>().SetTarget(transform);

            localPlayer = this;

            playerName = GameManager.Instance.playerNickname;
            nickname = playerName;
            view.RPC("UpdateNickname", RpcTarget.AllBuffered, playerName);

            Color randomColor = Random.ColorHSV();
            playerColor = randomColor;

            view.RPC("UpdatePlayerColor", RpcTarget.AllBuffered, randomColor.r, randomColor.g, randomColor.b);
        }
        else
        {
            playerName = nickname;
        }

        if (nicknameText != null)
        {
            nicknameText.text = playerName;
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        spriteRenderer.color = playerColor;
    }

    [PunRPC]
    public void UpdateNickname(string newNickname)
    {
        nickname = newNickname;

        if (nicknameText != null)
        {
            nicknameText.text = newNickname;
        }
    }

    [PunRPC]
    public void UpdatePlayerColor(float r, float g, float b)
    {
        playerColor = new Color(r, g, b);
        spriteRenderer.color = playerColor;
    }

    [PunRPC]
    public void UpdateHealthBar(float health)
    {
        currentHealth = health;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [PunRPC]
    public void UpdateScore(int points)
    {
        score += points;
        if (view.IsMine)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            ProcessInputs();
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking damage: " + damage);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        view.RPC("UpdateHealthBar", RpcTarget.AllBuffered, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (view.IsMine)
        {
            Respawn();
        }
    }

    // Respawn sem delay
    void Respawn()
    {
        Vector2 randomSpawnPosition = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
        );

        transform.position = randomSpawnPosition;

        currentHealth = maxHealth;
        view.RPC("UpdateHealthBar", RpcTarget.AllBuffered, currentHealth);
    }
}