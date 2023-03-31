using TMPro;
using UnityEngine;

public class RecourcesPanel : MonoBehaviour
{
    public TMP_Text chipsText;

    public RecourcesController _recources;

    public void UpdateChipsText()
    {
        chipsText.text = _recources.usedChipsCount + "/" + _recources.maxChipsCount;
    }
}
