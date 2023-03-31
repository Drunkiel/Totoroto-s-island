using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject[] teleports;
    public bool teleportUsed;

    public float cooldown;
    public float resCooldown;

    TriggerController[] _triggerControllers;

    void Start()
    {
        _triggerControllers = new TriggerController[] { teleports[0].GetComponent<TriggerController>(), teleports[1].GetComponent<TriggerController>() };
        resCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_triggerControllers[0].isTriggered || _triggerControllers[1].isTriggered) && !teleportUsed) TeleportPlayer();

        if (teleportUsed) ResetTimer();
    }

    void ResetTimer()
    {
        if (cooldown <= 0)
        {
            teleportUsed = false;
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void TeleportPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (_triggerControllers[0].isTriggered) player.transform.position = teleports[1].transform.position;
        if (_triggerControllers[1].isTriggered) player.transform.position = teleports[0].transform.position;

        teleportUsed = true;
    }
}
