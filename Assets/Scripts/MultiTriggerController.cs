using UnityEngine;

public class MultiTriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string[] objectsTag;

    void OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = true;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = false;
            }
        }
    }
}
