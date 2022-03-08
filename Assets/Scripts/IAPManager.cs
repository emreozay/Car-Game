using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string money500 = "com.sgamestudio.carparkourmegastuntgame.money500"; //Change it later!!!
    private string money1000 = "com.sgamestudio.carparkourmegastuntgame.money1000"; //Change it later!!!
    private string money1500 = "com.sgamestudio.carparkourmegastuntgame.money1500"; //Change it later!!!
    private string money2000 = "com.sgamestudio.carparkourmegastuntgame.money2000"; //Change it later!!!

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == money500)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 500);
            print("500 MONEY!!");
        }
        else if (product.definition.id == money1000)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 1000);
            print("1000 MONEY!!");
        }
        else if (product.definition.id == money1500)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 1500);
            print("1500 MONEY!!");
        }
        else if (product.definition.id == money2000)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 2000);
            print("2000 MONEY!!");
        }

        ShopManager.moneyTextEvent.Invoke();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print(product.definition.id + " failed because " + failureReason);
    }
}
