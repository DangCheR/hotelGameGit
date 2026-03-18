using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class AssetMGR : MonoBehaviour
{
    public Image spriteImage; // 用于显示加载的Sprite
    void Start()
    {
        AssetBundle MainAb = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ForPC");
        AssetBundleManifest manifest = MainAb.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        AssetBundle ModelAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/model");
        GameObject Sphere = ModelAB.LoadAsset<GameObject>("Sphere");

        foreach (var name in manifest.GetAllDependencies("model"))
        {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + name);

            Debug.Log($"AssetBundle: {name}");
        }

        // ModelAB.Unload(false); // 卸载AssetBundle但保留加载的资源
        Instantiate(Sphere, transform);
        StartCoroutine(AsyncLoadAsset());
    }

    IEnumerator AsyncLoadAsset()
    {
        AssetBundleCreateRequest ModelAB = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/sprite");
        yield return ModelAB;
        AssetBundleRequest request = ModelAB.assetBundle.LoadAssetAsync<Sprite>("horse");
        yield return request;
        Sprite horseSprite = request.asset as Sprite;
        spriteImage.sprite = horseSprite;
        yield return null;
    }

    public void UninstallAsset()
    {
        // 卸载所有，true包括已加载的资源
        AssetBundle.UnloadAllAssetBundles(true);
    }
}