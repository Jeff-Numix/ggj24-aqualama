using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public EquipmentType equipmentType;

    [Header("Debug")]
    new public Collider2D collider2D;
    public bool isInZone=false;
    // Start is called before the first frame update
    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(Player.Instance != null)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 boundPos = new Vector3(playerPosition.x, playerPosition.y, 0);

            if(!isInZone && collider2D.bounds.Contains(boundPos))
            {
                isInZone = true;
                Player.Instance.EquipObject(equipmentType);
                gameObject.SetActive(false);

            }
            else if(isInZone && !collider2D.bounds.Contains(boundPos))
            {
                isInZone = false;

            }
        }
    }
}
