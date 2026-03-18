using UnityEngine;
using System;
using UnityEngine.UI;


class BattleUI : MonoBehaviour
{
    public Slider progressBar; // 假设有一个进度条组件
    public Text UIdata; // 显示进度百分比的文本组件
    void Start()
    {
        var manager = FindObjectOfType<AsyncStudy>();
        manager.OnDataLoaded += OnBattleDataLoaded;
        manager.OnProgress += ShowProgress;
        manager.OnLoadError += error => Debug.LogError($"加载错误：{error}");

        manager.LoadNewDataAsync(); // 模拟点击加载按钮
    }
    public void OnBattleDataLoaded(BattleData data)
    {
        UIdata.text = $"加载完成！敌人数量：{data.a}";
    }

    public void ShowProgress(float progress)
    {
        progressBar.value = progress;
        // 更新加载进度UI
    }

    void OnDestroy()
    {
        // 记得取消订阅，防止内存泄漏
        var manager = FindObjectOfType<AsyncStudy>();
        if (manager != null)
        {
            manager.OnDataLoaded -= OnBattleDataLoaded;
            manager.OnProgress -= ShowProgress;
            manager.OnLoadError -= error => Debug.LogError($"加载错误：{error}");
        }
    }
}