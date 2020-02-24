using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float hitPoints = 100f;
    DeathHandler deathHandler;

    // Start is called before the first frame update
    void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            deathHandler.HandleDeath();
        }
    }
}
