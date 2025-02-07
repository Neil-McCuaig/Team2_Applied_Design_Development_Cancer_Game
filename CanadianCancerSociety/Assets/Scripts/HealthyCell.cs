using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyCell : MonoBehaviour
{
    public GameObject healthyCellPrefab;
    public float healthyCellSpeed = 2f;
    public int healthyCellCount = 10;
    private List<GameObject> healthyCells = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < healthyCellCount; i++)
        {
            SpawnCell(healthyCellPrefab);
        }
    }

    void SpawnCell(GameObject cellPrefab)
    {
        GameObject newCell = Instantiate(cellPrefab, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), Quaternion.identity);
        if (cellPrefab == healthyCellPrefab)
        {
            healthyCells.Add(newCell);
        }
    }
    void MoveHealthyCells()
    {
        foreach (var healthyCell in healthyCells)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (healthyCell != null)
            {
                healthyCell.transform.Translate(randomDirection * healthyCellSpeed);
            }
        }
    }
}
