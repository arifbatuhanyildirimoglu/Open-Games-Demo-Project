using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    private PlayerControls playerControls;
    private Vector3 movement;
    private float speed = 15f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector3>();
        playerControls.Gameplay.Move.canceled += ctx => movement = Vector3.zero;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector3 Movement
    {
        get => movement;
        set => movement = value;
    }
}
