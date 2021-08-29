using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static AsyncOperation m_asyncOperation;
    private static UnityAction m_finish;
    private static UnityAction<float> m_Progress;
    private static string m_targetScene;
    
    private GComponent _mainView;
    private GGraph _touchPanel;
    private GProgressBar _progressBar;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _mainView = this.GetComponent<UIPanel>().ui;
        _touchPanel = _mainView.GetChild("touch_panel").asGraph;
        _touchPanel.onClick.Add(delegate(EventContext context)
        {
            m_asyncOperation.allowSceneActivation = true;
        });
        _progressBar = _mainView.GetChild("progress").asProgress;
        
        StartCoroutine("LoadingScene");
    }

    private IEnumerator LoadingScene()
    {
        m_asyncOperation = SceneManager.LoadSceneAsync(m_targetScene); //异步加载1号场景
        m_asyncOperation.allowSceneActivation = false;                          //不允许场景立即激活//异步进度在 allowSceneActivation= false时，会卡在0.89999的一个值，这里乘以100转整形
        // 加载完毕后抛出事件
        m_asyncOperation.completed += delegate(AsyncOperation obj)
        {
            m_finish();
            m_Progress = null;
            m_finish = null;
            m_targetScene = null;
            m_asyncOperation = null;
        };
        while (!m_asyncOperation.isDone)
        {
            if (m_asyncOperation.progress < 0.9)
            {
                m_Progress(m_asyncOperation.progress);
                _progressBar.value = (int) (m_asyncOperation.progress * 100) + 10;
            }

            if (_progressBar.value >= 99)
            {
                _touchPanel.visible = true;
                _mainView.GetChild("text").asTextField.visible = true;
            }
            
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (m_AsyncOperation != null)
        {
            // 抛出加载进度
            if (m_Progress != null)
            {
                m_Progress(m_AsyncOperation.progress);
            }
        }*/
    }

    // <summary>
    // <param name = "name"> 场景名 </param>
    // <param name = "progress"> 回调加载进度 </param>
    // <param name = "finish"> 回调加载场景结束 </param>
    static public void LoadScene(string name, UnityAction<float> progress,
        UnityAction finish)
    {
        // 先加载进入
        SceneManager.LoadScene("LoadingScene");
        m_Progress = progress;
        m_finish = finish;
        m_targetScene = name;
    }

}
