using UnityEngine;

public class StuffPanelUI : MonoBehaviour
{
    private bool isUIOpen;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Button()
    {
        isUIOpen = !isUIOpen;

        if (isUIOpen) anim.Play("Close");
        else anim.Play("Open");
    }
}
