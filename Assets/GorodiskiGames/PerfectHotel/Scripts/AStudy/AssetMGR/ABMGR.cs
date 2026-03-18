using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABMGR : MonoBehaviour
{
    private ABMGR() { } // 私有构造函数，防止外部实例化
    private static ABMGR _instance;
    public static ABMGR instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("AssetBundleManager");
                _instance = go.AddComponent<ABMGR>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private string streamingPath = Application.streamingAssetsPath + "/";
    private string MainAbName
    {
        get
        {
#if UNITY_EDITOR
            return "ForPC";
#elif UNITY_ANDROID
            return "ForAndroid";
#else            
            return "ForIOS";
#endif
        }

    }
    private AssetBundle mainAB;
    private AssetBundleManifest manifest;
    private Dictionary<string, AssetBundle> ABDic = new Dictionary<string, AssetBundle>();

    // 加载AssetBundle并获取资源
    public AssetBundle LoadAssetBundle(string _bundle_name)
    {
        // 加载主AssetBundle和Manifest
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(streamingPath + MainAbName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        // 加载依赖项
        AssetBundle temp;
        string[] dependencies = manifest.GetAllDependencies(_bundle_name);
        foreach (var dep in dependencies)
        {
            if (!ABDic.ContainsKey(dep))
            {
                temp = AssetBundle.LoadFromFile(streamingPath + dep);
                ABDic[dep] = temp;
            }
        }

        if (!ABDic.ContainsKey(_bundle_name))
        {
            temp = AssetBundle.LoadFromFile(streamingPath + _bundle_name);
            ABDic[_bundle_name] = temp;
        }
        return ABDic[_bundle_name];
    }

    public object LoadAsset(string _bundle_name, string _asset_name, System.Type type)
    {
        AssetBundle ab = LoadAssetBundle(_bundle_name);
        return ab.LoadAsset(_asset_name, type);
    }
    public T LoadAsset<T>(string _bundle_name, string _asset_name) where T : Object
    {
        AssetBundle ab = LoadAssetBundle(_bundle_name);
        return ab.LoadAsset<T>(_asset_name);
    }


    public IEnumerator LoadAssetAsync<T>(string _bundle_name, string _asset_name, UnityAction<T> cb) where T : Object
    {
        AssetBundle ab = LoadAssetBundle(_bundle_name);
        AssetBundleRequest abr = ab.LoadAssetAsync<T>(_asset_name);
        yield return abr;
        cb(abr.asset as T);
    }

    // 卸载AssetBundle
    public void UnloadAssetBundle(string _bundle_name, bool unloadAllLoadedObjects = false)
    {
        if (ABDic.ContainsKey(_bundle_name))
        {
            ABDic[_bundle_name].Unload(unloadAllLoadedObjects);
            ABDic.Remove(_bundle_name);
        }
    }

    // 卸载所有AssetBundle
    public void UnloadAllAssetBundles(bool unloadAllLoadedObjects = false)
    {
        AssetBundle.UnloadAllAssetBundles(unloadAllLoadedObjects);
        ABDic.Clear();
        mainAB = null;
        manifest = null;
    }
}