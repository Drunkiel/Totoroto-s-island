using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject[] UIToClose;
    public bool isOpen;

    private void OpenCloseUI(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }

    public void Button()
    {
        if (!isOpen)
        {
            OpenCloseUI(mainUI, true);
            isOpen = true;
        }
        else
        {
            OpenCloseUI(mainUI, false);
            isOpen = false;
        }
    }

    public void CloseRestUI()
    {
        foreach (GameObject UI in UIToClose)
        {
            OpenCloseUI(UI, false);
        }
    }
}
