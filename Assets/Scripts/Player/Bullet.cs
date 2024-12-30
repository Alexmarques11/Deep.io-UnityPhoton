using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float lifetime;
    public float damage = 10f;

    void Awake()
    {
        if (!PhotonView.Get(this).IsMine)
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = mousePos - PlayerController.localPlayer.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PhotonView.Get(this).IsMine)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage(damage);

                if (playerController.currentHealth <= 0)
                {
                    PlayerController.localPlayer.view.RPC("UpdateScore", RpcTarget.All, 1);
                }
            }
        }

        PhotonNetwork.Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
    }
}
