using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string objectTag = "";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(objectTag))
        {
            isTriggered = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag(objectTag))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(objectTag))
        {
            isTriggered = false;
        }
    }
}
