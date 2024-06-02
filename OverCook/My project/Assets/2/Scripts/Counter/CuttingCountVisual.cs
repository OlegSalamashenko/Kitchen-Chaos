using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCountVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    
    [SerializeField] private CuttingCounter cuttingCount; 
    
    private Animator animator;
        
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCount.OnCut += CuttingCount_OnCut;
        
    }

    private void CuttingCount_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

    
}
