using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public CellManager cellManager;

    public GameObject cancerIdentifier;
    public GameObject immuneIdentifier;
    public GameObject healthyIdentifier;

    public GameObject tutorial;
    public GameObject immuneNextButton;
    public GameObject healthyNextButton;
    public GameObject exitNextButton;

    public Transform camOriginalPos;
    public Camera cam;
    public Vector3 offset = new Vector3(0,0,5);

    private void Start()
    {
        cellManager = FindAnyObjectByType<CellManager>();

        tutorial.SetActive(false);

        Invoke("TimeFreezeCancerCell", 7f);
    }

    public void TimeFreezeCancerCell()
    {
        tutorial.SetActive(true);

        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.cancerCells[0].transform.position - offset;
        cancerIdentifier.transform.position = cellManager.cancerCells[0].transform.position;
    }
    public void TimeFreezeImmuneCell()
    {
        immuneNextButton.SetActive(false);
        healthyNextButton.SetActive(true);
        cancerIdentifier.transform.position = new Vector3(50, 50, 0);

        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.immuneCells[0].transform.position - offset;
        immuneIdentifier.transform.position = cellManager.immuneCells[0].transform.position;
    }
    public void TimeFreezeHealthyCell()
    {
        healthyNextButton.SetActive(false);
        exitNextButton.SetActive(true);
        immuneIdentifier.transform.position = new Vector3(50, 50, 0);

        Time.timeScale = 0f;
        cam.orthographicSize = 4;
        cam.transform.position = cellManager.healthyCells[0].transform.position - offset;
        healthyIdentifier.transform.position = cellManager.healthyCells[0].transform.position;
    }
    public void DeactivateTimeFreeze()
    {
        tutorial.SetActive(false);

        Time.timeScale = 1f;
        cam.orthographicSize = 8;
        cam.transform.position = new Vector3(0, 0, -10);
        healthyIdentifier.transform.position = new Vector3(50, 50, 0);
    }

}
