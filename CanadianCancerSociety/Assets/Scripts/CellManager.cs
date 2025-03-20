using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    //Defaults: Immune cell speed 1.3, max speed 2, increases by 0.05. Cancer cell speed 0.4, max speed 0.85, increases by 0.05
    public GameObject healthyCellPrefab;
    public GameObject cancerCellPrefab;
    public GameObject immuneCellPrefab;

    public int healthyCellCount = 10;
    public int cancerCellCount = 1;
    public int immuneCellCount = 3;

    public float healthyCellSpeed;
    public float immuneCellSpeed = 0.0027f;
    public float immuneCellMaxSpeed = 0.005f;
    public float immuneSpeedIncreaseRate = 0.001f;
    public float immuneAttackRange = 2f;
    public float cancerCellDetectionRange = 5f;

    public Transform healthyCellSpawn; 
    public Transform cancerCellSpawn;
    public Transform immuneCellSpawn;
    public float spawnRange;

    public List<GameObject> healthyCells = new List<GameObject>();
    public List<GameObject> cancerCells = new List<GameObject>();
    public List<GameObject> immuneCells = new List<GameObject>();

    private bool canSpawnCancerCell = true;
    public float cancerAttackCooldown = 0.2f;
    public float cancerAttackMaxCooldown = 0.7f;
    public float cancerAttackIncreaseRate = 0.1f;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public int numOfWaves;
    public float spawnInterval = 2f; 
    public float waveInterval = 7f;
    private bool useSpawnPoint1 = true; 
    private bool finishWave = false;

    public int wavesUntilCellCount;
    public float cancerCellSpeed = 3f; 
    public float maxCancerSpeed = 3f;       
    public float cancerSpeedIncreaseRate = 0.2f; 

    public int currentWave = 1;
    public int maxWave;
    public TextMeshProUGUI waveText;

    public int pickUpTotal;
    public int pickUpMax;
    public List<Transform> spawnPoints;
    public GameObject pickUpPrefab;
    public float pickUpDropInterval = 2f;
    public int maxPickUp = 2;
    public int wavesUntilPickUp = -2;

    public GameObject winScreen;
    public GameObject loseScreen;
    MiniGameDialog dialog;

    void Start()
    {
        waveText.text = "Wave: " + currentWave + "/8";
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        dialog = FindAnyObjectByType<MiniGameDialog>();
       
        // Spawn Healthy Cells
        for (int i = 0; i < healthyCellCount; i++)
        {
            SpawnCell(healthyCellPrefab);
        }

        // Spawn Immune Cells
        for (int i = 0; i < immuneCellCount; i++)
        {
            SpawnCell(immuneCellPrefab);
        }

        UpdateUI();
        // Start spawning objects in a coroutine
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnPickUp());
    }
    IEnumerator SpawnWaves()
    {
        // Continuously spawn objects at intervals
        while (numOfWaves > 0)
        {
            // Alternate spawn point
            Transform currentSpawnPoint = useSpawnPoint1 ? spawnPoint1 : spawnPoint2;

            // Instantiate each object in the list at the current spawn point
            for (int i = 0; i < cancerCellCount; i++)
            {
                GameObject newCell = Instantiate(cancerCellPrefab, currentSpawnPoint.position, Quaternion.identity);
                cancerCells.Add(newCell);
            }
      
            // Toggle spawn point for next time
            useSpawnPoint1 = !useSpawnPoint1;
            numOfWaves--;

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
        finishWave = true;
    }
    IEnumerator SpawnPickUp()
    {
        // Continuously spawn objects at intervals
        while (wavesUntilPickUp > 0)
        {
            for (int i = 0; i < maxPickUp; i++)
            {
                    int randomIndex = Random.Range(0, spawnPoints.Count);
                    Transform randomSpawnPoint = spawnPoints[randomIndex];

                    Instantiate(pickUpPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            }
            wavesUntilPickUp--;
            // Wait for the next spawn interval
            yield return new WaitForSeconds(pickUpDropInterval);
        }
    }

    void EndWave()
    {
        currentWave += 1;
        waveText.text = "Wave: " + currentWave + "/8";
        dialog.TriggerDialog();
        numOfWaves = (currentWave + 2);

        // Increase the speed by the rate
        cancerCellSpeed += cancerSpeedIncreaseRate;
        if (cancerCellSpeed > maxCancerSpeed)
        {
            cancerCellSpeed = maxCancerSpeed;
        }

        cancerAttackCooldown -= cancerAttackIncreaseRate;
        if (cancerAttackCooldown < cancerAttackMaxCooldown)
        {
            cancerAttackCooldown = cancerAttackMaxCooldown;
        }

        wavesUntilCellCount += 1;
        if (wavesUntilCellCount >= 3)
        {
            cancerCellCount++;
            wavesUntilCellCount = 0;
        }

        wavesUntilPickUp += 1;
        if (wavesUntilPickUp > 1)
        {
            wavesUntilPickUp = 1;
            
        }
        if (currentWave >= 4)
        {
            maxPickUp = 2;
        }
        else if (currentWave == 6)
        {
            maxPickUp = 3;
        }
        StartCoroutine(SpawnNewWave());
    }
    IEnumerator SpawnNewWave()
    {
        yield return new WaitForSeconds(waveInterval);
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnPickUp());
    }

    void Update()
    {
        if (currentWave < 9)
        {
            MoveHealthyCells();
            MoveCancerCells();
            MoveImmuneCells();

            if (finishWave && cancerCells.Count == 0)
            {
                finishWave = false;
                EndWave();
            }
        }
        else if (currentWave >= 9)
        { 
            winScreen.SetActive(true);
        }
        if (healthyCells.Count == 0)
        {
            loseScreen.SetActive(true);
        }
    }

    public void SpawnCell(GameObject cellPrefab)
    {
        if (cellPrefab == healthyCellPrefab)
        {
            Vector3 randomPosition = GetHealthyCellRandomPosition();
            GameObject newCell = Instantiate(cellPrefab, randomPosition, Quaternion.identity);
            healthyCells.Add(newCell);
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
        immuneCellSpeed += immuneSpeedIncreaseRate;
        if (immuneCellSpeed > immuneCellMaxSpeed)
        {
            immuneCellSpeed = immuneCellMaxSpeed;
        }

        pickUpTotal += 1;
        if (pickUpTotal >= pickUpMax)
        {
            GameObject newCell = Instantiate(immuneCellPrefab, pickupPosition.position, Quaternion.identity);
            immuneCells.Add(newCell);
            pickUpTotal = 0;
        }
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
                cancerCell.transform.position = Vector3.MoveTowards(cancerCell.transform.position, targetHealthyCell.transform.position, cancerCellSpeed * Time.deltaTime);

                // Turn healthy cell into cancer cell if close enough
                if (Vector2.Distance(cancerCell.transform.position, targetHealthyCell.transform.position) < 1.7f)
                {
                    // Only spawn a new cancer cell if allowed
                    if (canSpawnCancerCell)
                    {
                        // Turn healthy cell into cancer cell
                        healthyCells.Remove(targetHealthyCell);  // Remove the healthy cell from the list
                        Destroy(targetHealthyCell);
                        score -= 10;
                        UpdateUI();
                        GameObject newCell = Instantiate(cancerCellPrefab, cancerCell.transform.position, Quaternion.identity);
                        cancerCells.Add(newCell);
                        canSpawnCancerCell = false;  // Disable further cancer cell spawn for now
                        StartCoroutine(EnableCancerCellSpawn());  // Wait before enabling spawn again
                    }
                }
            }
        }
    }

    void MoveImmuneCells()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Keep the movement in 2D (ensure no change in Z axis)

        foreach (var immuneCell in immuneCells)
        {
            Vector3 direction = (mousePosition - immuneCell.transform.position).normalized;
            immuneCell.transform.position = Vector3.MoveTowards(immuneCell.transform.position, mousePosition, immuneCellSpeed * Time.deltaTime);

            // Check if immune cell is near cancer cells to attack
            foreach (var cancerCell in cancerCells)
            {
                if (Vector2.Distance(immuneCell.transform.position, cancerCell.transform.position) < immuneAttackRange)
                {
                    score += 100;
                    UpdateUI();
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

    Vector3 GetImmuneCellRandomPosition()
    {
        // Random position within the bounds relative to the areaTransform
        float randomX = Random.Range(immuneCellSpawn.position.x - spawnRange, immuneCellSpawn.position.x + spawnRange);
        float randomY = Random.Range(immuneCellSpawn.position.y - spawnRange, immuneCellSpawn.position.y + spawnRange);

        // Return a Vector3 with only X and Y for 2D
        return new Vector3(randomX, randomY, 0f); // Z is set to 0 for 2D
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }
}
