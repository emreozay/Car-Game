using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] carModels;
    [SerializeField] private int currentCarIndex;
    [SerializeField] private Button buyButton;
    [SerializeField] private CarBlueprint[] cars;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button startButton;


    void Start()
    {
        PlayerPrefs.SetInt("Money", 1000); // Don't forget to delete!!!
        UpdateMoneyText();

        foreach (CarBlueprint car in cars)
        {
            if (car.price == 0)
                car.isUnlocked = true;
            else
                car.isUnlocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;
        }

        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        foreach (GameObject car in carModels)
        {
            car.SetActive(false);
        }
        carModels[currentCarIndex].SetActive(true);
    }

    void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 20);
            UpdateMoneyText();
        }
    }

    public void ChangeNextCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex++;
        if (currentCarIndex == carModels.Length)
            currentCarIndex = 0;

        carModels[currentCarIndex].SetActive(true);

        CarBlueprint c = cars[currentCarIndex];
        if (!c.isUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void ChangePreviousCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex--;
        if (currentCarIndex == -1)
            currentCarIndex = carModels.Length - 1;

        carModels[currentCarIndex].SetActive(true);

        CarBlueprint c = cars[currentCarIndex];
        if (!c.isUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void UnlockCar()
    {
        CarBlueprint c = cars[currentCarIndex];

        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);

        c.isUnlocked = true;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - c.price);
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);
    }

    private void UpdateUI()
    {
        CarBlueprint c = cars[currentCarIndex];

        if (c.isUnlocked)
        {
            startButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);

            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - $" + c.price;

            if (c.price <= PlayerPrefs.GetInt("Money", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
}
