using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public void ActivateTimeFreeze()
    {
        Time.timeScale = 0f;
    }
    public void DeactivateTimeFreeze()
    {
        Time.timeScale = 1f;
    }
}
