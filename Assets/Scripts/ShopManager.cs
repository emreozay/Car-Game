using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] carModels;
    [SerializeField] private int currentCarIndex;

    void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        foreach (GameObject car in carModels)
        {
            car.SetActive(false);
        }
        carModels[currentCarIndex].SetActive(true);
    }

    void Update()
    {
        
    }

    public void ChangeNextCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex++;
        if (currentCarIndex == carModels.Length)
            currentCarIndex = 0;

        carModels[currentCarIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void ChangePreviousCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex--;
        if (currentCarIndex == -1)
            currentCarIndex = carModels.Length - 1;

        carModels[currentCarIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }
}
