using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AAMGR : MonoBehaviour
{
    public AssetReference ar;
    public AssetLabelReference _label;
    public Image spriteImage; // 用于显示加载的Sprite
    void Start()
    {
        LoadWithLable();
        CallBackLoad();
        LoadRomote();
    }


    // 使用回调实例化
    private void CallBackLoad()
    {
        AsyncOperationHandle<GameObject> res = Addressables.LoadAssetAsync<GameObject>("Assets/Study/AnyAsset/Sphere.prefab");
        res.Completed += instan;
    }

    void instan(AsyncOperationHandle<GameObject> handle)
    {
        var prefab = handle.Result;
        Instantiate(prefab, transform);
    }

    // 使用异步等待加载并实例化
    private async void AsyncLoad2()
    {
        GameObject gameObject = await Addressables.LoadAssetAsync<GameObject>("Assets/Study/AnyAsset/Sphere.prefab").Task;
        GameObject cur = Instantiate(gameObject);
    }

    private void load3()
    {
        ar.LoadAssetAsync<GameObject>();
    }

    private async void LoadRomote()
    {
        Sprite s = await Addressables.LoadAssetAsync<Sprite>("Assets/Study/AnyAsset/horse.png").Task;
        spriteImage.sprite = s;
    }

    // 按照label加载
    private void LoadWithLable()
    {
        Addressables.LoadAssetsAsync<GameObject>(_label, (prefab) =>
        {
            Debug.Log("实例化" + prefab.name);
            Instantiate(prefab);
        });
    }
}