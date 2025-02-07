using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    CellManager cellManager;

    private void Start()
    {
        cellManager = FindAnyObjectByType<CellManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Immune"))
        {
            // Destroy this pickup object
            Destroy(gameObject);
            cellManager.SpawnCell(cellManager.immuneCellPrefab);
        }
    }
}
