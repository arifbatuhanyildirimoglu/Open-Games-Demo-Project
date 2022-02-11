using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private Vector3 offset = new Vector3(0,13.7f,-12.5f);

    [SerializeField]
    private GameObject player;
    
    void Start()
    {
        
    }

    void Update()
    {
           
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset ;
    }
}
