using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectInteraction : MonoBehaviour
{
    public GameObject buildingUI;
    public GameObject mineableUI;
    public LayerMask[] layerMasks;
    public ProductionID _productionID;

    public bool CheckIfClickedBuilding(bool openUI)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Collider[] hitBuildings = Physics.OverlapSphere(hit.point, 1.0f, layerMasks[0]);
            Collider[] hitMineable = Physics.OverlapSphere(hit.point, 1.0f, layerMasks[1]);
            if (hitBuildings.Length > 0)
            {
                if (openUI) OpenBuildingUI(hitBuildings);
                return true;
            }
            else if (hitMineable.Length > 0)
            {
                if (openUI) OpenMineableUI(hitMineable);
                return true;
            }
             else return false;
        }
        else return false;
    }

    public void OpenBuildingUI(Collider[] hitBuildings)
    {
        if (!hitBuildings[0].GetComponent<PlacableObject>().isPlaced) return;
        if (!hitBuildings[0].GetComponent<ProductionID>()) return;

        _productionID = hitBuildings[0].GetComponent<ProductionID>();
        if (_productionID.isInProduction)
        {
            buildingUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            _productionID.GetProduction();
        }
        else buildingUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

        buildingUI.GetComponent<OpenClose>().Button();
        buildingUI.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(() => hitBuildings[0].GetComponent<PlacableObject>().Move());

        //Set building menu
        BuildingID buildingID = hitBuildings[0].GetComponent<BuildingID>();

        //Set production list
        SetProductionList();
    }

    private void SetProductionList()
    {
        GameObject productionCards = buildingUI.transform.GetChild(1).GetChild(0).gameObject;

        for (int i = 0; i < productionCards.transform.childCount; i++)
        {
            int j = i;
            productionCards.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = _productionID._productionCards[i].sprite;
            productionCards.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = _productionID._productionCards[i].name;
            productionCards.transform.GetChild(i).GetChild(2).GetComponent<Button>().onClick.AddListener(() => _productionID.SetProduction(j));
        }
    }

    public void OpenMineableUI(Collider[] hitMineables)
    {
        mineableUI.GetComponent<OpenClose>().Button();
    }
}
