using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private GComponent _mainView;

    private GComponent newContainer;
    
    private Controller _viewController;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainView = this.GetComponent<UIPanel>().ui;
        _viewController = _mainView.GetController("c1");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
