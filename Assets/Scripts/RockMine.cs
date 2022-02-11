using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private int rockCapacity;

    private Transform leftRock;
    private Transform rightRock;
    private Transform midRock;

    void Start()
    {
        rockCapacity = 3;
        midRock = transform.GetChild(0);
        leftRock = transform.GetChild(1);
        rightRock = transform.GetChild(2);
    }

    void Update()
    {
        switch (rockCapacity)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                rightRock.gameObject.SetActive(false);
                break;
            case 2:
                leftRock.gameObject.SetActive(false);
                break;
        }
    }

    public int RockCapacity => rockCapacity;
    public void DecreaseRockCapacity()
    {
        rockCapacity--;
    }
}
