using UnityEngine;
using System.IO;

public class IOsave : MonoBehaviour
{
    public string filePath = "IOsaveData.txt";
    void Start()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);
        // File.WriteAllText(fullPath, "IO存储测试");
        FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate);
        byte[] data = System.Text.Encoding.UTF8.GetBytes("IO Byte字节存储测试");
        fs.Write(data, 0, data.Length);
        fs.Close();
    }
}