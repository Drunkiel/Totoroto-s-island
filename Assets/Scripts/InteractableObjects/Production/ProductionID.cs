using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductionID : MonoBehaviour
{
    public bool isInProduction;
    public int productionID;
    private DateTime startedTime;
    public DateTime endTime;

    private Transform productionUI;

    public ProductionCard[] _productionCards;
    private ProductionUI _productionUI;

    void Start()
    {
        _productionUI = GameObject.Find("BuildingPanel").GetComponent<ProductionUI>();
        productionUI = GameObject.Find("BuildingPanel").transform.GetChild(1);
    }

    void FixedUpdate()
    {
        if (_productionUI.isProductionEnded)
        {
            productionUI.GetChild(1).GetChild(3).GetChild(3).gameObject.SetActive(true);
            productionUI.GetChild(1).GetChild(3).GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
            productionUI.GetChild(1).GetChild(3).GetChild(3).GetComponent<Button>().onClick.AddListener(() => CollectProduction());
        }
    }

    public void SetProduction(int i)
    {
        productionID = i;
        _productionUI.productionID = i;
        startedTime = DateTime.Now;

        endTime = startedTime;
        endTime = endTime.AddDays(_productionCards[i].daysToEnd);
        endTime = endTime.AddHours(_productionCards[i].hoursToEnd);
        endTime = endTime.AddMinutes(_productionCards[i].minutesToEnd);
        endTime = endTime.AddSeconds(_productionCards[i].secondsToEnd);

        isInProduction = true;
        productionUI.GetChild(1).gameObject.SetActive(true);
        GetProduction();
    }

    public void GetProduction()
    {
        productionUI.GetChild(1).GetChild(3).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        productionUI.GetChild(1).GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(() => CancelProduction());
        _productionUI.productionID = productionID;
        _productionUI.endTime = endTime;
    }

    public void CollectProduction()
    {
        CancelProduction();
        productionUI.GetChild(1).GetChild(3).GetChild(3).gameObject.SetActive(false);
    }

    public void CancelProduction()
    {
        isInProduction = false;
        productionUI.GetChild(1).gameObject.SetActive(false);
        productionUI.GetChild(0).gameObject.SetActive(true);
        endTime = new DateTime();
        GetProduction();
    }
}
