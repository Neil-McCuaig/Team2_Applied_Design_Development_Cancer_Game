using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorDetection : MonoBehaviour
{
    private bool notDamaged = true;
    public float cooldownTimer = 1f;

    SurgeryGameManager gameManager;
    ScreenShake screenShake;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        gameManager = FindFirstObjectByType<SurgeryGameManager>();
        screenShake = FindAnyObjectByType<ScreenShake>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Method to handle mouse clicks
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (notDamaged)
            {
                screenShake.TriggerScreenShake();
                notDamaged = false;
                spriteRenderer.color = Color.red;
                gameManager.DecreaseScore(70f);
                StartCoroutine(DamagedCooldown()); 
            }
        }
    }

    IEnumerator DamagedCooldown()
    {
        yield return new WaitForSeconds(cooldownTimer);
        spriteRenderer.color = new Color(255, 255, 255);
        notDamaged = true;
    }
}
