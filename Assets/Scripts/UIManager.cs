using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject woodAmountTextArcher;
    [SerializeField] private GameObject rockAmountTextArcher;
    [SerializeField] private GameObject goldAmountTextArcher;
    [SerializeField] private GameObject woodAmountTextSpearman;
    [SerializeField] private GameObject rockAmountTextSpearman;
    [SerializeField] private GameObject goldAmountTextSpearman;

    [SerializeField] private GameObject archerBuilding;
    [SerializeField] private GameObject spearmanBuilding;

    [SerializeField] private GameObject woodAmountText;
    [SerializeField] private GameObject rockAmountText;
    [SerializeField] private GameObject goldAmountText;

    private int woodNeededForArcher;
    private int goldNeededForArcher;
    private int rockNeededForSpearman;
    private int goldNeededForSpearman;

    private GameObject gameManager;

    [SerializeField] private GameObject gameOverPanel;

    private GameObject player;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        player = GameObject.FindWithTag("Player");

        woodNeededForArcher = 3;
        goldNeededForArcher = 3;
        rockNeededForSpearman = 3;
        goldNeededForSpearman = 3;
    }

    void Update()
    {
        
        UpdateItemListText();
        UpdateRequiredItemsForArcherBuilding();
        UpdateRequiredItemsForSpearmanBuilding();

        if (gameManager.GetComponent<GameManager>().IsArcherBuildingCompleted)
        {
            woodAmountTextArcher.GetComponent<Text>().text = woodNeededForArcher.ToString();
            goldAmountTextArcher.GetComponent<Text>().text = goldNeededForArcher.ToString();
        }

        if (gameManager.GetComponent<GameManager>().IsSpearmanBuildingCompleted)
        {
            rockAmountTextSpearman.GetComponent<Text>().text = rockNeededForSpearman.ToString();
            goldAmountTextSpearman.GetComponent<Text>().text = goldNeededForSpearman.ToString();
        }

        if (woodNeededForArcher == 0 && goldNeededForArcher == 0)
        {
            gameManager.GetComponent<GameManager>().ArcherCanBeInstantiated = true;
        }

        if (rockNeededForSpearman == 0 && goldNeededForSpearman == 0)
        {
            gameManager.GetComponent<GameManager>().SpearmanCanBeInstantiated = true;
        }
    }

    private void UpdateItemListText()
    {
        int woodAmount = 0;
        int rockAmount = 0;
        int goldAmount = 0;

        for (int i = 0; i < player.GetComponent<Player>().ItemList.Count; i++)
        {

            GameObject item = player.GetComponent<Player>().ItemList[i];

            if (item.CompareTag("Wood"))
                woodAmount++;
            else if (item.CompareTag("Rock"))
                rockAmount++;
            else if (item.CompareTag("Gold"))
                goldAmount++;

        }

        woodAmountText.GetComponent<Text>().text = woodAmount.ToString();
        rockAmountText.GetComponent<Text>().text = rockAmount.ToString();
        goldAmountText.GetComponent<Text>().text = goldAmount.ToString();
    }

    private void UpdateRequiredItemsForArcherBuilding()
    {
        int unbuildedWoodAmountArcher = 0;
        int unbuildedRockAmountArcher = 0;
        int unbuildedGoldAmountArcher = 0;
        for (int i = 0; i < archerBuilding.transform.childCount; i++)
        {
            GameObject instantChild = archerBuilding.transform.GetChild(i).gameObject;

            if (instantChild.CompareTag("Wood") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedWoodAmountArcher++;
            }
            else if (instantChild.CompareTag("Rock") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedRockAmountArcher++;
            }
            else if (instantChild.CompareTag("Gold") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedGoldAmountArcher++;
            }
        }
        woodAmountTextArcher.GetComponent<Text>().text = unbuildedWoodAmountArcher.ToString();
        rockAmountTextArcher.GetComponent<Text>().text = unbuildedRockAmountArcher.ToString();
        goldAmountTextArcher.GetComponent<Text>().text = unbuildedGoldAmountArcher.ToString();
    }

    private void UpdateRequiredItemsForSpearmanBuilding()
    {
        int unbuildedWoodAmountSpearman = 0;
        int unbuildedRockAmountSpearman = 0;
        int unbuildedGoldAmountSpearman = 0;
        for (int i = 0; i < spearmanBuilding.transform.childCount; i++)
        {
            GameObject instantChild = spearmanBuilding.transform.GetChild(i).gameObject;

            if (instantChild.CompareTag("Wood") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedWoodAmountSpearman++;
            }
            else if (instantChild.CompareTag("Rock") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedRockAmountSpearman++;
            }
            else if (instantChild.CompareTag("Gold") && !instantChild.GetComponent<BuildingPart>().IsBuilded)
            {
                unbuildedGoldAmountSpearman++;
            }
        }

        
        woodAmountTextSpearman.GetComponent<Text>().text = unbuildedWoodAmountSpearman.ToString();
        rockAmountTextSpearman.GetComponent<Text>().text = unbuildedRockAmountSpearman.ToString();
        goldAmountTextSpearman.GetComponent<Text>().text = unbuildedGoldAmountSpearman.ToString();
    }
    
    public GameObject WoodAmountTextArcher
    {
        get => woodAmountTextArcher;
        set => woodAmountTextArcher = value;
    }

    public GameObject RockAmountTextArcher
    {
        get => rockAmountTextArcher;
        set => rockAmountTextArcher = value;
    }

    public GameObject GoldAmountTextArcher
    {
        get => goldAmountTextArcher;
        set => goldAmountTextArcher = value;
    }

    public GameObject WoodAmountTextSpearman
    {
        get => woodAmountTextSpearman;
        set => woodAmountTextSpearman = value;
    }

    public GameObject RockAmountTextSpearman
    {
        get => rockAmountTextSpearman;
        set => rockAmountTextSpearman = value;
    }

    public GameObject GoldAmountTextSpearman
    {
        get => goldAmountTextSpearman;
        set => goldAmountTextSpearman = value;
    }

    public int WoodNeededForArcher
    {
        get => woodNeededForArcher;
        set => woodNeededForArcher = value;
    }

    public int GoldNeededForArcher
    {
        get => goldNeededForArcher;
        set => goldNeededForArcher = value;
    }

    public int RockNeededForSpearman
    {
        get => rockNeededForSpearman;
        set => rockNeededForSpearman = value;
    }

    public int GoldNeededForSpearman
    {
        get => goldNeededForSpearman;
        set => goldNeededForSpearman = value;
    }

    public GameObject GameOverPanel => gameOverPanel;
}