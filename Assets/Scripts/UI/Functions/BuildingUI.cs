using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public Card[] buildingCards;

    [SerializeField]
    private bool[] actualActiveCards;

    public GameObject cardPrefab;
    public GameObject parent;

    void Awake()
    {
        actualActiveCards = new bool[buildingCards.Length];
    }

    void Start()
    {
        for (int i = 0; i < buildingCards.Length; i++)
        {
            BuildingID _buildingID = BuildingSystem.instance.buildingsPrefabs[i].GetComponent<BuildingID>();

            buildingCards[i].id = _buildingID.id;
            buildingCards[i].price = _buildingID.price;
            buildingCards[i].requiredLvl = _buildingID.requiredLvl;
            buildingCards[i].buildingName = _buildingID.buildingName;
            buildingCards[i].showcaseImage = _buildingID.showcaseImage;
        }
    }

    public void SpawnBuildingCards()
    {
        if (CheckIfActive(0)) return;
        DestroyCards();

        for (int i = 0; i < buildingCards.Length; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, parent.transform);
            newCard.transform.GetChild(0).GetComponent<Image>().sprite = buildingCards[i].showcaseImage;
            newCard.transform.GetChild(1).GetComponent<TMP_Text>().text = buildingCards[i].buildingName;
            int a = i;
            newCard.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => BuildingSystem.instance.InitializeWithObject(BuildingSystem.instance.buildingsPrefabs[a]));
        }
    }

    private bool CheckIfActive(int number)
    {
        if (actualActiveCards[number]) return true;
        else
        {
            actualActiveCards[number] = true;

            for (int i = 0; i < actualActiveCards.Length; i++)
            {
                if (actualActiveCards[i] != actualActiveCards[number]) actualActiveCards[i] = false;
            }

            return false;
        }
    }

    private void DestroyCards()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
}
