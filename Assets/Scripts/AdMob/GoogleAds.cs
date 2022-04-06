using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine.Events;

public class GoogleAds : MonoBehaviour
{
    InterstitialAdGameObject interstitialAd;
    RewardedAdGameObject rewardedAd;

    public static UnityEvent adMobInterstitialAdEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize((initStatus) =>
        {
            Debug.Log("Initialized MobileAds");
        });
    }

    private void Start()
    {
        interstitialAd = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("Interstitial Ad");

        rewardedAd = MobileAds.Instance
            .GetAd<RewardedAdGameObject>("Rewarded Ad");

        interstitialAd.LoadAd();
        rewardedAd.LoadAd();

        if (adMobInterstitialAdEvent == null)
            adMobInterstitialAdEvent = new UnityEvent();

        adMobInterstitialAdEvent.AddListener(AdMobShowInterstitial);
    }

    public void RewardEarned()
    {
        Debug.Log("Unity Ads Rewarded Ad Completed");

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 125);
        ShopManager.moneyTextEvent.Invoke();

        rewardedAd.LoadAd();
    }

    public void AdMobShowInterstitial()
    {
        // Display an interstitial ad
        interstitialAd.ShowIfLoaded();
        print("BURADA!!!");
    }

    public void AdClosed()
    {
        Time.timeScale = 0;
        interstitialAd.LoadAd();
    }
}
