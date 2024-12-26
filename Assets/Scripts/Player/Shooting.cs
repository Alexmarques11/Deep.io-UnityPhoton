using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timeBetweenFiring;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePosition - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenFiring)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if (Input.GetMouseButton(0) && canFire)
            {
                canFire = false;
                PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
            }
        }

    }
}
