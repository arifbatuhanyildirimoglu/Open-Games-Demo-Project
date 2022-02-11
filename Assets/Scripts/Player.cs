using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject archerBuilding;
    [SerializeField] private GameObject spearmanBuilding;

    private List<GameObject> itemList = new List<GameObject>();

    private GameObject gameManager;
    private GameObject uiManager;

    private Animator animator;

    private bool isMoving;

    private PlayerMovement playerMovement;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        uiManager = GameObject.Find("UI Manager");
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        StayInBoundaries();

        if (playerMovement.Movement != Vector3.zero)
        {
            isMoving = true;
            transform.forward = playerMovement.Movement;
            animator.SetBool("isIdling", false);
            animator.SetBool("isRunning", true);
        }
        else if (!animator.GetBool("isBuilding"))
        {
            isMoving = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", true);
        }
    }

    private void StayInBoundaries()
    {
        float minimumValue = -225f;
        float maximumValue = 225f;

        if (transform.position.x < minimumValue)
            transform.position = new Vector3(minimumValue, transform.position.y, transform.position.z);
        if (transform.position.x > maximumValue)
            transform.position = new Vector3(maximumValue, transform.position.y, transform.position.z);
        if (transform.position.z < minimumValue)
            transform.position = new Vector3(transform.position.x, transform.position.y, minimumValue);
        if (transform.position.z > maximumValue)
            transform.position = new Vector3(transform.position.x, transform.position.y, maximumValue);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree") && !isMoving)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", false);
            if (itemList.Count == 0)
            {
                if (other.gameObject.GetComponent<Tree>().WoodCapacity > 0)
                    StartCoroutine(ProduceWood(2.5f, other));
            }
            else
            {
                if (other.gameObject.GetComponent<Tree>().WoodCapacity > 0)
                    StartCoroutine(ProduceWood(itemList[itemList.Count - 1].transform.position.y + 1.5f, other));
            }
        }

        if (other.CompareTag("Rock Mine") && !isMoving)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", false);
            if (itemList.Count == 0)
            {
                if (other.gameObject.GetComponent<RockMine>().RockCapacity > 0)
                    StartCoroutine(ProduceRock(2.5f, other));
            }
            else
            {
                if (other.gameObject.GetComponent<RockMine>().RockCapacity > 0)
                    StartCoroutine(ProduceRock(itemList[itemList.Count - 1].transform.position.y + 1.5f, other));
            }
        }

        if (other.CompareTag("Gold Mine") && !isMoving)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", false);
            if (itemList.Count == 0)
            {
                if (other.gameObject.GetComponent<GoldMine>().GoldCapacity > 0)
                    StartCoroutine(ProduceGold(2.5f, other));
            }
            else
            {
                if (other.gameObject.GetComponent<GoldMine>().GoldCapacity > 0)
                    StartCoroutine(ProduceGold(itemList[itemList.Count - 1].transform.position.y + 1.5f, other));
            }
        }

        if (other.CompareTag("Archer Zone") && !isMoving)
        {
            if (itemList.Count != 0)
            {
                if (itemList[itemList.Count - 1].CompareTag("Wood") && uiManager.GetComponent<UIManager>()
                        .WoodAmountTextArcher.GetComponent<Text>().text.Equals("0") ||
                    itemList[itemList.Count - 1].CompareTag("Rock") && uiManager.GetComponent<UIManager>()
                        .RockAmountTextArcher.GetComponent<Text>().text.Equals("0") ||
                    itemList[itemList.Count - 1].CompareTag("Gold") && uiManager.GetComponent<UIManager>()
                        .GoldAmountTextArcher.GetComponent<Text>().text.Equals("0"))
                {
                    playerMovement.enabled = true;
                }
                else
                {
                    playerMovement.enabled = false;
                    animator.SetBool("isBuilding", true);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isIdling", false);
                    if (gameManager.GetComponent<GameManager>().IsArcherBuildingCompleted)
                    {
                        StartCoroutine(DeployBuildingPartArcherSoldier());
                    }
                    else
                    {
                        StartCoroutine(DeployBuildingPartArcher());
                    }
                }
                
            }
            else if (itemList.Count == 0 && animator.GetBool("isBuilding"))
            {
                playerMovement.enabled = true;
                animator.SetBool("isBuilding", false);
            }
        }

        if (other.CompareTag("Spearman Zone") && !isMoving)
        {
            if (itemList.Count != 0)
            {
                if (itemList[itemList.Count - 1].CompareTag("Wood") && uiManager.GetComponent<UIManager>()
                        .WoodAmountTextSpearman.GetComponent<Text>().text.Equals("0") ||
                    itemList[itemList.Count - 1].CompareTag("Rock") && uiManager.GetComponent<UIManager>()
                        .RockAmountTextSpearman.GetComponent<Text>().text.Equals("0") ||
                    itemList[itemList.Count - 1].CompareTag("Gold") && uiManager.GetComponent<UIManager>()
                        .GoldAmountTextSpearman.GetComponent<Text>().text.Equals("0"))
                {
                    playerMovement.enabled = true;
                }
                else
                {
                    playerMovement.enabled = false;
                    animator.SetBool("isBuilding", true);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isIdling", false);
                    if (gameManager.GetComponent<GameManager>().IsSpearmanBuildingCompleted)
                    {
                        StartCoroutine(DeployBuildingPartSpearmanSoldier());
                    }
                    else
                    {
                        StartCoroutine(DeployBuildingPartSpearman());
                    }
                }
            }
            else if (itemList.Count == 0 && animator.GetBool("isBuilding"))
            {
                playerMovement.enabled = true;
                animator.SetBool("isBuilding", false);
            }
        }

        if (other.CompareTag("Garbage Truck") && !isMoving)
        {
            if (itemList.Count != 0)
            {
                playerMovement.enabled = false;
                animator.SetBool("isBuilding", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdling", false);
                StartCoroutine(RecycleItems(other));
            }
            else if (itemList.Count == 0 && animator.GetBool("isBuilding"))
            {
                playerMovement.enabled = true;
                animator.SetBool("isBuilding", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree") || other.CompareTag("Rock Mine") || other.CompareTag("Gold Mine"))
        {
            animator.SetBool("isCollecting", false);
            animator.SetBool("isIdling", true);
        }

        if (other.CompareTag("Archer Zone") || other.CompareTag("Spearman Zone") || other.CompareTag("Garbage Truck"))
        {
            animator.SetBool("isBuilding", false);
            animator.SetBool("isIdling", true);
        }
    }

    private bool isDeployingSpearmanSoldier = true;

    IEnumerator DeployBuildingPartSpearmanSoldier()
    {
        GameObject lastItem = itemList[itemList.Count - 1];
        String itemTag = lastItem.tag;
        int rockNeeded =
            Convert.ToInt32(uiManager.GetComponent<UIManager>().RockAmountTextSpearman.GetComponent<Text>().text);
        int goldNeeded =
            Convert.ToInt32(uiManager.GetComponent<UIManager>().GoldAmountTextSpearman.GetComponent<Text>().text);

        if (isDeployingSpearmanSoldier)
        {
            if (itemTag.Equals("Rock") && rockNeeded > 0)
            {
                lastItem.transform.SetParent(null);
                lastItem.GetComponent<Collectable>().DeployDestination = new Vector3(33f, 0, 24f);
                lastItem.GetComponent<Collectable>().IsDeployable = true;
                lastItem.GetComponent<Collectable>().OriginalObject = spearmanBuilding;
                itemList.Remove(lastItem);
                rockNeeded--;
                uiManager.GetComponent<UIManager>().RockNeededForSpearman = rockNeeded;
            }
            else if (itemTag.Equals("Gold") && goldNeeded > 0)
            {
                lastItem.transform.SetParent(null);
                lastItem.GetComponent<Collectable>().DeployDestination = new Vector3(33f, 0, 24f);
                lastItem.GetComponent<Collectable>().IsDeployable = true;
                lastItem.GetComponent<Collectable>().OriginalObject = spearmanBuilding;
                itemList.Remove(lastItem);
                goldNeeded--;
                uiManager.GetComponent<UIManager>().GoldNeededForSpearman = goldNeeded;
            }

            isDeployingSpearmanSoldier = false;
            yield return new WaitForSeconds(0.2f);
            isDeployingSpearmanSoldier = true;
        }
    }

    private bool isDeployingArcherSoldier = true;

    IEnumerator DeployBuildingPartArcherSoldier()
    {
        GameObject lastItem = itemList[itemList.Count - 1];
        String itemTag = lastItem.tag;
        int woodNeeded =
            Convert.ToInt32(uiManager.GetComponent<UIManager>().WoodAmountTextArcher.GetComponent<Text>().text);
        int goldNeeded =
            Convert.ToInt32(uiManager.GetComponent<UIManager>().GoldAmountTextArcher.GetComponent<Text>().text);

        if (isDeployingArcherSoldier)
        {
            if (itemTag.Equals("Wood") && woodNeeded > 0)
            {
                lastItem.transform.SetParent(null);
                lastItem.GetComponent<Collectable>().DeployDestination = new Vector3(-18.5f, 0, 24.5f);
                lastItem.GetComponent<Collectable>().IsDeployable = true;
                lastItem.GetComponent<Collectable>().OriginalObject = archerBuilding;
                itemList.Remove(lastItem);
                woodNeeded--;
                uiManager.GetComponent<UIManager>().WoodNeededForArcher = woodNeeded;
            }
            else if (itemTag.Equals("Gold") && goldNeeded > 0)
            {
                lastItem.transform.SetParent(null);
                lastItem.GetComponent<Collectable>().DeployDestination = new Vector3(-18.5f, 0, 24.5f);
                lastItem.GetComponent<Collectable>().IsDeployable = true;
                lastItem.GetComponent<Collectable>().OriginalObject = archerBuilding;
                itemList.Remove(lastItem);
                goldNeeded--;
                uiManager.GetComponent<UIManager>().GoldNeededForArcher = goldNeeded;
            }

            isDeployingArcherSoldier = false;
            yield return new WaitForSeconds(0.2f);
            isDeployingArcherSoldier = true;
        }
    }

    private bool isDeployingArcher = true;

    IEnumerator DeployBuildingPartArcher()
    {
        GameObject lastItem = itemList[itemList.Count - 1];
        String itemTag = lastItem.tag;

        if (isDeployingArcher)
        {
            for (int i = 0; i < archerBuilding.transform.childCount; i++)
            {
                Transform instantChildTransform = archerBuilding.transform.GetChild(i);
                if (instantChildTransform.CompareTag(itemTag) &&
                    !instantChildTransform.GetComponent<BuildingPart>().IsBuilded)
                {
                    instantChildTransform.GetComponent<BuildingPart>().IsBuilded = true;
                    lastItem.transform.SetParent(null);
                    lastItem.GetComponent<Collectable>().DeployDestination = instantChildTransform.position;
                    lastItem.GetComponent<Collectable>().IsDeployable = true;
                    lastItem.GetComponent<Collectable>().OriginalObject = instantChildTransform.gameObject;
                    itemList.RemoveAt(itemList.Count - 1);
                    break;
                }
            }

            isDeployingArcher = false;
            yield return new WaitForSeconds(0.2f);
            isDeployingArcher = true;
        }
    }

    private bool isDeployingSpearman = true;

    IEnumerator DeployBuildingPartSpearman()
    {
        GameObject lastItem = itemList[itemList.Count - 1];
        String itemTag = lastItem.tag;

        if (isDeployingSpearman)
        {
            for (int i = 0; i < spearmanBuilding.transform.childCount; i++)
            {
                Transform instantChildTransform = spearmanBuilding.transform.GetChild(i);
                if (instantChildTransform.CompareTag(itemTag) &&
                    !instantChildTransform.GetComponent<BuildingPart>().IsBuilded)
                {
                    instantChildTransform.GetComponent<BuildingPart>().IsBuilded = true;
                    lastItem.transform.SetParent(null);
                    lastItem.GetComponent<Collectable>().DeployDestination = instantChildTransform.position;
                    lastItem.GetComponent<Collectable>().IsDeployable = true;
                    lastItem.GetComponent<Collectable>().OriginalObject = instantChildTransform.gameObject;
                    itemList.Remove(lastItem);
                    break;
                }
            }

            isDeployingSpearman = false;
            yield return new WaitForSeconds(0.2f);
            isDeployingSpearman = true;
        }
    }

    private bool isItemRecyling = true;

    IEnumerator RecycleItems(Collider other)
    {
        for (int i = itemList.Count - 1; i >= 0; i--)
        {
            if (isItemRecyling)
            {
                itemList[i].transform.SetParent(null);
                itemList[i].gameObject.GetComponent<Collectable>().DeployDestination = other.transform.position;
                itemList[i].gameObject.GetComponent<Collectable>().IsDeployable = true;
                itemList[i].gameObject.GetComponent<Collectable>().OriginalObject = other.gameObject;
                itemList.RemoveAt(i);
                isItemRecyling = false;
                yield return new WaitForSeconds(0.2f);
                isItemRecyling = true;
            }
        }
    }

    private bool isWoodProducing = true;

    IEnumerator ProduceWood(float y, Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        if (isWoodProducing)
        {
            Vector3 pos = transform.position + (-transform.forward * 2f);
            pos.y = y;
            animator.SetTrigger("isCollecting");
            GameObject newWood = Instantiate(woodPrefab, pos, Quaternion.LookRotation(transform.forward));
            newWood.transform.SetParent(transform);
            itemList.Add(newWood);
            other.gameObject.GetComponent<Tree>().DecreaseWoodCapacity();
            isWoodProducing = false;
            yield return new WaitForSeconds(0.5f);
            isWoodProducing = true;
        }
    }

    private bool isRockProducing = true;

    IEnumerator ProduceRock(float y, Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        if (isRockProducing)
        {
            Vector3 pos = transform.position + (-transform.forward * 2f);
            pos.y = y;
            animator.SetTrigger("isCollecting");
            GameObject newRock = Instantiate(rockPrefab,
                pos, Quaternion.LookRotation(transform.forward));
            newRock.transform.SetParent(transform);
            itemList.Add(newRock);
            other.gameObject.GetComponent<RockMine>().DecreaseRockCapacity();
            isRockProducing = false;
            yield return new WaitForSeconds(0.5f);
            isRockProducing = true;
        }
    }

    private bool isGoldProducing = true;

    IEnumerator ProduceGold(float y, Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        if (isGoldProducing)
        {
            Vector3 pos = transform.position + (-transform.forward * 2f);
            pos.y = y;
            animator.SetTrigger("isCollecting");
            GameObject newGold = Instantiate(goldPrefab,
                pos, Quaternion.LookRotation(transform.forward));
            newGold.transform.SetParent(transform);
            itemList.Add(newGold);
            other.gameObject.GetComponent<GoldMine>().DecreaseGoldCapacity();
            isGoldProducing = false;
            yield return new WaitForSeconds(0.5f);
            isGoldProducing = true;
        }
    }

    public List<GameObject> ItemList => itemList;

    public Animator Animator => animator;
}