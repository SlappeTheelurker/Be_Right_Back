using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SafeHouseController : MonoBehaviour {
    public GameObject craftMenu, player, healthBar, weapon, winText, flash;
    public Button fRButton, hPButton, cureButton, dimensionButton;
    public int fireRateCost, fireRateCostAdded, healthCost, healthCostAdded, antidoteCost;

    private List<GameObject> houses;

    //if a safeHouse already exists, destroy self
    private void Awake()
    {
        int safeHouseCount = 0;
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == "SafeHouse")
            {
                safeHouseCount++;
            }
        }

        if (safeHouseCount >= 2)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        fRButton.onClick.AddListener(FRUp);
        hPButton.onClick.AddListener(HPUp);
        cureButton.onClick.AddListener(CraftCure);
        dimensionButton.onClick.AddListener(TravelDimensions);
        DontDestroyOnLoad(transform.gameObject);
    }

    void FRUp()
    {
        if (player.GetComponent<PlayerController>().resourceAmount >= fireRateCost)
        {
            player.GetComponent<PlayerController>().resourceAmount -= fireRateCost;
            weapon.GetComponent<Weapon>().firingRate *= 0.75f;
            weapon.GetComponent<Weapon>().fireRateAnim += 0.25f;
            fireRateCost += fireRateCostAdded;
            GameObject.Find("FRText").gameObject.GetComponent<Text>().text = "Craft: " + fireRateCost;
        }
    }

    void HPUp()
    {
        if (player.GetComponent<PlayerController>().resourceAmount >= healthCost)
        {
            player.GetComponent<PlayerController>().resourceAmount -= healthCost;
            player.GetComponent<HealthController>().currentHealth += 4;
            healthBar.GetComponent<HeartController>().ChangeHeartAmount(4);
            healthCost += healthCostAdded;
            GameObject.Find("HPText").gameObject.GetComponent<Text>().text = "Craft: " + healthCost;
        }
    }

    void CraftCure()
    {
        if (player.GetComponent<PlayerController>().resourceAmount >= antidoteCost)
        {
            winText.SetActive(true);
        }
    }

    void TravelDimensions()
    {
        GameObject newFlash = Instantiate(flash, new Vector3(0f, 0f, 0f), Quaternion.identity);
        newFlash.transform.SetParent(GameObject.Find("UICanvas").gameObject.transform, false);
        if (SceneManager.GetActiveScene().name == "FirstLevel")
        {
            SceneManager.LoadScene("GrassDimension");
        }
        else
        {
            GameObject.Find("LevelGenerator").gameObject.GetComponent<LevelGenerator>().NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            craftMenu.SetActive(true);
            GameObject.Find("FRText").gameObject.GetComponent<Text>().text = "Craft: " + fireRateCost;
            GameObject.Find("HPText").gameObject.GetComponent<Text>().text = "Craft: " + healthCost;
            GameObject.Find("AText").gameObject.GetComponent<Text>().text = "Craft: " + antidoteCost;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            craftMenu.SetActive(false);
        }
    }

    public bool CraftMenuOpen()
    {
        if (craftMenu.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
