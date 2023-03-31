using UnityEngine;

public class AutoSize : MonoBehaviour
{
    private float[] newSize = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        //x
        if (Mathf.Floor(GetScale().x)%2 != 0) newSize[0] = Mathf.Floor(GetScale().x) + 2f;
        else newSize[0] = Mathf.Floor(GetScale().x) + 1f;

        //y
        newSize[1] = Mathf.Floor(GetScale().y) + 1f;

        //z
        if (Mathf.Floor(GetScale().z) % 2 != 0) newSize[2] = Mathf.Floor(GetScale().z) + 2f;
        else newSize[2] = Mathf.Floor(GetScale().z) + 1f;

        transform.localScale = new Vector3(newSize[0], newSize[1], newSize[2]);
    }

    private Vector3 GetScale()
    {
        return transform.parent.GetComponent<BoxCollider>().size;
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
