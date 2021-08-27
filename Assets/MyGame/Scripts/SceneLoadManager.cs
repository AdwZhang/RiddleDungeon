using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static AsyncOperation m_AsyncOperation;
    private static UnityAction<float> m_Progress;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AsyncOperation != null)
        {
            // 抛出加载进度
            if (m_Progress != null)
            {
                m_Progress(m_AsyncOperation.progress);
            }
        }
    }

    // <summary>
    // <param name = "name"> 场景名 </param>
    // <param name = "progress"> 回调加载进度 </param>
    // <param name = "finish"> 回调加载场景结束 </param>
    static public void LoadScene(string name, UnityAction<float> progress,
        UnityAction finish)
    {
        // 先加载进入
        new GameObject("#SceneLoadManager").AddComponent<SceneLoadManager>();
        m_AsyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        m_Progress = progress;
        
        // 加载完毕后抛出事件
        m_AsyncOperation.completed += delegate(AsyncOperation obj)
        {
            finish();
            m_AsyncOperation = null;
            m_Progress = null;
        };
    }
    
    
}
