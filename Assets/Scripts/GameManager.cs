using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject archerBuilding;
    [SerializeField] private GameObject spearmanBuilding;

    private bool isArcherBuildingCompleted;
    private bool isSpearmanBuildingCompleted;

    private bool archerCanBeInstantiated;
    private bool spearmanCanBeInstantiated;

    private GameObject uiManager;

    private int instantiatedArcher;
    private int instantiatedSpearman;

    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject spearmanPrefab;

    [SerializeField] private GameObject archerSpawnPos1;
    [SerializeField] private GameObject archerSpawnPos2;
    [SerializeField] private GameObject archerSpawnPos3;
    [SerializeField] private GameObject spearmanSpawnPos1;
    [SerializeField] private GameObject spearmanSpawnPos2;
    [SerializeField] private GameObject spearmanSpawnPos3;

    private GameObject[] archers;
    private GameObject[] spearmen;

    void Start()
    {
        isArcherBuildingCompleted = false;
        isSpearmanBuildingCompleted = false;

        archerCanBeInstantiated = false;
        spearmanCanBeInstantiated = false;

        uiManager = GameObject.Find("UI Manager");

        instantiatedArcher = 0;
        instantiatedSpearman = 0;
    }

    void Update()
    {
        archers = GameObject.FindGameObjectsWithTag("Archer");
        spearmen = GameObject.FindGameObjectsWithTag("Spearman");

        isArcherBuildingCompleted = true;
        for (int i = 0; i < archerBuilding.transform.childCount; i++)
        {
            if (!archerBuilding.transform.GetChild(i).GetComponent<BuildingPart>().IsBuilded)
            {
                isArcherBuildingCompleted = false;
                break;
            }
        }

        isSpearmanBuildingCompleted = true;
        for (int i = 0; i < spearmanBuilding.transform.childCount; i++)
        {
            if (!spearmanBuilding.transform.GetChild(i).GetComponent<BuildingPart>().IsBuilded)
            {
                isSpearmanBuildingCompleted = false;
                break;
            }
        }

        if (archerCanBeInstantiated)
        {
            switch (instantiatedArcher)
            {
                case 0:
                    Instantiate(archerPrefab, archerSpawnPos1.transform.position,
                        Quaternion.LookRotation(archerSpawnPos1.transform.forward));
                    break;
                case 1:
                    Instantiate(archerPrefab, archerSpawnPos2.transform.position,
                        Quaternion.LookRotation(archerSpawnPos2.transform.forward));
                    for (int i = 0; i < archers.Length; i++)
                    {
                        archers[i].GetComponent<Archer>().IsHappy = true;
                    }

                    break;
                case 2:
                    Instantiate(archerPrefab, archerSpawnPos3.transform.position,
                        Quaternion.LookRotation(archerSpawnPos3.transform.forward));
                    for (int i = 0; i < archers.Length; i++)
                    {
                        archers[i].GetComponent<Archer>().IsHappy = true;
                    }

                    break;
            }

            uiManager.GetComponent<UIManager>().WoodNeededForArcher = 3;
            uiManager.GetComponent<UIManager>().GoldNeededForArcher = 3;
            instantiatedArcher++;
            archerCanBeInstantiated = false;
        }

        if (spearmanCanBeInstantiated)
        {
            switch (instantiatedSpearman)
            {
                case 0:
                    Instantiate(spearmanPrefab, spearmanSpawnPos1.transform.position, Quaternion.LookRotation(spearmanSpawnPos1.transform.forward));
                    break;
                case 1:
                    Instantiate(spearmanPrefab, spearmanSpawnPos2.transform.position, Quaternion.LookRotation(spearmanSpawnPos2.transform.forward));
                    for (int i = 0; i < spearmen.Length; i++)
                    {
                        spearmen[i].GetComponent<Spearman>().IsHappy = true;
                    }
                    break;
                case 2:
                    Instantiate(spearmanPrefab, spearmanSpawnPos3.transform.position, Quaternion.LookRotation(spearmanSpawnPos3.transform.forward));
                    for (int i = 0; i < spearmen.Length; i++)
                    {
                        spearmen[i].GetComponent<Spearman>().IsHappy = true;
                    }
                    break;
            }

            uiManager.GetComponent<UIManager>().RockNeededForSpearman = 3;
            uiManager.GetComponent<UIManager>().GoldNeededForSpearman = 3;
            instantiatedSpearman++;
            spearmanCanBeInstantiated = false;
        }

        if (instantiatedArcher == 3)
        {
            archerCanBeInstantiated = false;
            uiManager.GetComponent<UIManager>().WoodAmountTextArcher.GetComponent<Text>().text = "0";
            uiManager.GetComponent<UIManager>().GoldAmountTextArcher.GetComponent<Text>().text = "0";
        }

        if (instantiatedSpearman == 3)
        {
            spearmanCanBeInstantiated = false;
            uiManager.GetComponent<UIManager>().RockAmountTextSpearman.GetComponent<Text>().text = "0";
            uiManager.GetComponent<UIManager>().GoldAmountTextSpearman.GetComponent<Text>().text = "0";
        }

        if (instantiatedArcher == 3 && instantiatedSpearman == 3)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        uiManager.GetComponent<UIManager>().GameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }

    public bool IsArcherBuildingCompleted
    {
        get => isArcherBuildingCompleted;
    }

    public bool IsSpearmanBuildingCompleted
    {
        get => isSpearmanBuildingCompleted;
    }

    public bool ArcherCanBeInstantiated
    {
        get => archerCanBeInstantiated;
        set => archerCanBeInstantiated = value;
    }

    public bool SpearmanCanBeInstantiated
    {
        get => spearmanCanBeInstantiated;
        set => spearmanCanBeInstantiated = value;
    }
}