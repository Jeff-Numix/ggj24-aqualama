using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
    // Create an Event that will be called when the player enters the zone.
    public UnityEvent onPlayerEnterZone = null;
    public UnityEvent onPlayerExitZone = null;
    public bool triggerWhenForceEnterZone=true;
    

    [Header("Debug")]
    new public Collider2D collider2D;
    public bool isInZone=false;
    // Start is called before the first frame update
    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance != null)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 boundPos = new Vector3(playerPosition.x, playerPosition.y, 0);

            if(!isInZone && collider2D.bounds.Contains(boundPos))
            {
                isInZone = true;
                if(onPlayerEnterZone != null)
                {
                    onPlayerEnterZone.Invoke();
                }
            }
            else if(isInZone && !collider2D.bounds.Contains(boundPos))
            {
                isInZone = false;
                if(onPlayerExitZone != null)
                {
                    onPlayerExitZone.Invoke();
                }
            }
        }
    }

    public void ForceExitZone(){
        isInZone = false;
        if(onPlayerExitZone != null)
        {
            onPlayerExitZone.Invoke();
        }
    }

    public void ForceEnterZone(){
        isInZone = true;
        if(onPlayerEnterZone != null && triggerWhenForceEnterZone)
        {
            onPlayerEnterZone.Invoke();
        }
    }
}
