using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    private int xMin;
    [SerializeField]
    private int xMax;
    [SerializeField]
    private int yMin;
    [SerializeField]
    private int yMax;
    [SerializeField]
    private float smoothSpeed;
    private Vector3 smoothPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        int xPos = Mathf.Clamp((int)player.position.x, xMin, xMax);
        int yPos = Mathf.Clamp((int)player.position.y, yMin, yMax);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(xPos, yPos, this.transform.position.z), smoothSpeed);
        transform.position = smoothPos;
    }
}
