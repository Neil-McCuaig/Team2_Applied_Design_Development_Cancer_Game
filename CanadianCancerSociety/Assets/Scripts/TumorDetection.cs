using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorDetection : MonoBehaviour
{
    private bool notDamaged = true;
    public float cooldownTimer = 1f;

    SurgeryGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<SurgeryGameManager>();
    }
    // Method to handle mouse clicks
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (notDamaged)
            {
                notDamaged = false;
                gameManager.DecreaseScore(300f);
                StartCoroutine(DamagedCooldown()); 
            }
        }
    }

    IEnumerator DamagedCooldown()
    {
        yield return new WaitForSeconds(cooldownTimer);  
        notDamaged = true;
    }
}
