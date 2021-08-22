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
        GButton startGameBtn = _mainView.GetChild("start_button").asButton;
        startGameBtn.onClick.Add(startNewGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void startNewGame()
    {
        SceneLoadManager.LoadScene("SampleScene", delegate(float progress)
        {
            Debug.LogFormat("加载进度：{0}", progress);
        }, delegate(){
            Debug.Log("加载结束");
        } );
    }
}
