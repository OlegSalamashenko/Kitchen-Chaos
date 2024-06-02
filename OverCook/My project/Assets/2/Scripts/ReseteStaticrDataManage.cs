using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseteStaticrDataManage : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaceCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
