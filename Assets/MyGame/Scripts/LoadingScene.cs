using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private GComponent _mainView;

    private GObject _touchPanel;

    private GProgressBar _progressBar;
    
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainView = this.GetComponent<UIPanel>().ui;
        _touchPanel = _mainView.GetChild("touch_panel");
        _progressBar = _mainView.GetChild("progress").asProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
