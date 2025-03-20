using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public CellManager cellManager;

    public GameObject cancerIdentifier;
    public GameObject immuneIdentifier;
    public GameObject healthyIdentifier;

    public Transform camOriginalPos;
    public Camera cam;
    public Vector3 offset = new Vector3(0,0,5);

    private void Start()
    {
        cellManager = FindAnyObjectByType<CellManager>();
    }

    public void TimeFreezeCancerCell()
    {
        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.cancerCells[0].transform.position - offset;
        cancerIdentifier.transform.position = cellManager.cancerCells[0].transform.position;
    }
    public void TimeFreezeImmuneCell()
    {
        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.immuneCells[0].transform.position - offset;
        immuneIdentifier.transform.position = cellManager.immuneCells[0].transform.position;
    }
    public void TimeFreezeHealthyCell()
    {
        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.healthyCells[0].transform.position - offset;
        healthyIdentifier.transform.position = cellManager.healthyCells[0].transform.position;
    }
    public void DeactivateTimeFreeze()
    {
        Time.timeScale = 1f;
        cam.orthographicSize = 8;
        cam.transform.position = new Vector3(0, 0, -10);
        cancerIdentifier.transform.position = new Vector3(50, 50, 0);
        immuneIdentifier.transform.position = new Vector3(50, 50, 0);
        healthyIdentifier.transform.position = new Vector3(50, 50, 0);
    }

}
