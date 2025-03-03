using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public GameObject healthyCellPrefab;
    public GameObject cancerCellPrefab;
    public GameObject immuneCellPrefab;

    public int healthyCellCount = 10;
    public int cancerCellCount = 5;
    public int immuneCellCount = 3;

    public float healthyCellSpeed = 2f;
    public float cancerCellSpeed = 3f;
    public float immuneCellSpeed = 5f;
    public float immuneAttackRange = 2f;
    public float cancerCellDetectionRange = 5f;

    public Transform healthyCellSpawn; // The transform defining the area
    public Transform cancerCellSpawn;
    public Transform immuneCellSpawn;
    public float spawnRange;

    private List<GameObject> healthyCells = new List<GameObject>();
    private List<GameObject> cancerCells = new List<GameObject>();
    private List<GameObject> immuneCells = new List<GameObject>();

    private bool canSpawnCancerCell = true;  // Track if a cancer cell can be spawned
    public float cancerAttackCooldown = 0.7f;  

    // The score variable
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Spawn Healthy Cells
        for (int i = 0; i < healthyCellCount; i++)
        {
            SpawnCell(healthyCellPrefab);
        }

        // Spawn Cancer Cells
        for (int i = 0; i < cancerCellCount; i++)
        {
            SpawnCell(cancerCellPrefab);
        }

        // Spawn Immune Cells
        for (int i = 0; i < immuneCellCount; i++)
        {
            SpawnCell(immuneCellPrefab);
        }

        UpdateScore();
    }

    void Update()
    {
        MoveHealthyCells();
        MoveCancerCells();
        MoveImmuneCells();
    }

    public void SpawnCell(GameObject cellPrefab)
    {
        if (cellPrefab == healthyCellPrefab)
        {
            Vector3 randomPosition = GetHealthyCellRandomPosition();
            GameObject newCell = Instantiate(cellPrefab, randomPosition, Quaternion.identity);
            healthyCells.Add(newCell);
        }
        else if (cellPrefab == cancerCellPrefab)
        {
            Vector3 randomPosition = GetCancerCellRandomPosition();
            GameObject newCell = Instantiate(cellPrefab, randomPosition, Quaternion.identity);
            cancerCells.Add(newCell);
        }
        else if (cellPrefab == immuneCellPrefab)
        {
            Vector3 randomPosition = GetImmuneCellRandomPosition();
            GameObject newCell = Instantiate(cellPrefab, randomPosition, Quaternion.identity);
            immuneCells.Add(newCell);
        }
    }

    public void PickupFunction(Transform pickupPosition)
    {
        GameObject newCell = Instantiate(immuneCellPrefab, pickupPosition.position, Quaternion.identity);
        immuneCells.Add(newCell);
    }

    void MoveHealthyCells()
    {
        foreach (var healthyCell in healthyCells)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            healthyCell.transform.Translate(randomDirection * healthyCellSpeed * Time.deltaTime);
        }
    }

    void MoveCancerCells()
    {
        foreach (var cancerCell in cancerCells.ToList())
        {
            GameObject targetHealthyCell = GetNearestHealthyCell(cancerCell);
            if (targetHealthyCell != null)
            {
                // Move towards the healthy cell
                Vector2 direction = (targetHealthyCell.transform.position - cancerCell.transform.position).normalized;
                cancerCell.transform.position = Vector3.MoveTowards(cancerCell.transform.position, targetHealthyCell.transform.position, cancerCellSpeed);

                // Turn healthy cell into cancer cell if close enough
                if (Vector2.Distance(cancerCell.transform.position, targetHealthyCell.transform.position) < 1.7f)
                {
                    // Only spawn a new cancer cell if allowed
                    if (canSpawnCancerCell)
                    {
                        // Turn healthy cell into cancer cell
                        healthyCells.Remove(targetHealthyCell);  // Remove the healthy cell from the list
                        Destroy(targetHealthyCell);
                        UpdateScore();
                        GameObject newCell = Instantiate(cancerCellPrefab, cancerCell.transform.position, Quaternion.identity);
                        cancerCells.Add(newCell);
                        canSpawnCancerCell = false;  // Disable further cancer cell spawn for now
                        StartCoroutine(EnableCancerCellSpawn());  // Wait before enabling spawn again
                    }
                }
            }
        }
        cancerCellCount = cancerCells.Count;
    }

    void MoveImmuneCells()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Keep the movement in 2D (ensure no change in Z axis)

        foreach (var immuneCell in immuneCells)
        {
            Vector3 direction = (mousePosition - immuneCell.transform.position).normalized;
            immuneCell.transform.position = Vector3.MoveTowards(immuneCell.transform.position, mousePosition, immuneCellSpeed);

            // Check if immune cell is near cancer cells to attack
            foreach (var cancerCell in cancerCells)
            {
                if (Vector2.Distance(immuneCell.transform.position, cancerCell.transform.position) < immuneAttackRange)
                {
                    // Destroy cancer cell (immune cell attacks)
                    Destroy(cancerCell);
                    cancerCells.Remove(cancerCell);
                    break;
                }
            }
        }
        immuneCellCount = immuneCells.Count;
    }

    GameObject GetNearestHealthyCell(GameObject cancerCell)
    {
        float closestDistance = float.MaxValue;
        GameObject nearestHealthyCell = null;

        foreach (var healthyCell in healthyCells)
        {
            float distance = Vector2.Distance(cancerCell.transform.position, healthyCell.transform.position);
            if (distance < closestDistance && distance < cancerCellDetectionRange)
            {
                closestDistance = distance;
                nearestHealthyCell = healthyCell;
            }
        }

        return nearestHealthyCell;
    }

    // Coroutine to allow spawning of cancer cells again after a short delay
    IEnumerator EnableCancerCellSpawn()
    {
        yield return new WaitForSeconds(cancerAttackCooldown);  // Delay for a second before allowing spawn again
        canSpawnCancerCell = true;
    }

    Vector3 GetHealthyCellRandomPosition()
    {
        // Random position within the bounds relative to the areaTransform
        float randomX = Random.Range(healthyCellSpawn.position.x - spawnRange, healthyCellSpawn.position.x + spawnRange);
        float randomY = Random.Range(healthyCellSpawn.position.y - spawnRange, healthyCellSpawn.position.y + spawnRange);

        // Return a Vector3 with only X and Y for 2D
        return new Vector3(randomX, randomY, 0f); // Z is set to 0 for 2D
    }

    Vector3 GetCancerCellRandomPosition()
    {
        // Random position within the bounds relative to the areaTransform
        float randomX = Random.Range(cancerCellSpawn.position.x - spawnRange, cancerCellSpawn.position.x + spawnRange);
        float randomY = Random.Range(cancerCellSpawn.position.y - spawnRange, cancerCellSpawn.position.y + spawnRange);

        // Return a Vector3 with only X and Y for 2D
        return new Vector3(randomX, randomY, 0f); // Z is set to 0 for 2D
    }

    Vector3 GetImmuneCellRandomPosition()
    {
        // Random position within the bounds relative to the areaTransform
        float randomX = Random.Range(immuneCellSpawn.position.x - spawnRange, immuneCellSpawn.position.x + spawnRange);
        float randomY = Random.Range(immuneCellSpawn.position.y - spawnRange, immuneCellSpawn.position.y + spawnRange);

        // Return a Vector3 with only X and Y for 2D
        return new Vector3(randomX, randomY, 0f); // Z is set to 0 for 2D
    }

    void UpdateScore()
    {
        // Count the remaining game objects
        int remainingObjects = healthyCells.Count;

        // Add 100 points for each remaining object
        score = remainingObjects * 100;

        scoreText.text = "Score: " + score;
    }
}
