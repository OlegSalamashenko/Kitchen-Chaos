using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCountVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainerCount containerCount; 
    
    private Animator animator;
        
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCount.OnPlayerGrabbedObject += ContainerCount_OnPlayerGrabbedObject;
    }

    private void ContainerCount_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
