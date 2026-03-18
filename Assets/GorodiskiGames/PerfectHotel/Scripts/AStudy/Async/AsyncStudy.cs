
using UnityEngine;
using System;
using System.Threading.Tasks;

public class AsyncStudy : MonoBehaviour
{
    public int Enemies = 0;
    public Action<BattleData> OnDataLoaded;//加载完成事件
    public Action<float> OnProgress;//加载进度事件
    public Action<string> OnLoadError;//加载错误事件
    void Update()
    {
        CheckAttack();
        CheckDamage();
    }

    // 模拟UI按钮点击事件，触发异步加载
    public void LoadNewDataAsync()
    {
        _ = LoadDataCoroutine("t");
    }

    // 模拟一个异步加载数据的协程，包含错误处理和进度更新
    async Task LoadDataCoroutine(string dataId)
    {
        try
        {
            // 1. 通知开始加载
            Debug.Log($"开始加载：{dataId}");
            
            // 2. 执行异步加载（不阻塞主线程）
            var data = await LoadBattleDataAsync(OnProgress);
            
            // 3. 加载完成，通知监听者
            OnDataLoaded?.Invoke(data);
            Debug.Log($"加载完成：{dataId}");
        }
        catch (Exception ex)
        {
            // 4. 错误处理
            OnLoadError?.Invoke(ex.Message);
            Debug.LogError($"加载失败：{ex.Message}");
        }
    }

    // 模拟异步加载数据的过程，包含进度回调
    async Task<BattleData> LoadBattleDataAsync(Action<float> progressCallback)
    {
        progressCallback?.Invoke(0.1f);
        await Task.Delay(200);  // 模拟网络/IO
        
        progressCallback?.Invoke(0.3f);
        await Task.Delay(200);;
        
        progressCallback?.Invoke(0.6f);
        await Task.Delay(200);
        
        progressCallback?.Invoke(0.8f);
        // var assets = await LoadAssetsAsync(dataId);
        
        progressCallback?.Invoke(1.0f);
        
        return new BattleData { a = 1, b = 2 };
    }

    //模拟攻击
    void CheckAttack()
    {
        Debug.Log($"模拟攻击，敌人数量：{Enemies}");
    }

    //模拟检查伤害
    void CheckDamage()
    {
        Debug.Log($"模拟检查伤害，敌人数量：{Enemies}");
    }
    
}
public struct BattleData
{
    public int a;
    public int b;
}