using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private PlayerController pc;
    private float looker;
    private float lookerClamp;

    private void Start()
    {
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        looker = -Input.GetAxis("Mouse Y") * pc.sensitivity;
        lookerClamp = Mathf.Clamp(looker, -89, 89);
        transform.eulerAngles += new Vector3(lookerClamp, 0, 0);
    }
}
