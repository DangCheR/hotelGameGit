using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;

class Excelsave : MonoBehaviour       
{
    List<PlayerData> playerDataList;

    public string excelPath = "Excel/玩家数据.xlsx";

    void Start()
    {
        InitExcelSheet();
    }
    public void InitExcelSheet()
    {
        LoadExcelData();
    }
    public void LoadExcelData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, excelPath);
        
        if (!File.Exists(fullPath))
        {
            // 文件不存在，创建新文件并初始化数据
            playerDataList = new List<PlayerData>()
            {
                new PlayerData() { name = "玩家1", level = 10, health = 100 },
                new PlayerData() { name = "玩家2", level = 20, health = 80 },
                new PlayerData() { name = "玩家3", level = 15, health = 90 }
            };
            File.Create(fullPath).Close(); // 必须Close()，否则文件会被占用
            // 创建 Excel 文件并写入数据
            WriteExcelData();
        }
        else
        {
            // 文件存在，加载数据
            FileInfo fileInfo = new FileInfo(fullPath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["PlayerData"];
                if (worksheet == null)
                {
                    Debug.LogError("工作表 'PlayerData' 不存在！");
                    return;
                }
                
                int rowCount = worksheet.Dimension.Rows;
                playerDataList = new List<PlayerData>();

                for (int i = 2; i <= rowCount; i++)
                {
                    PlayerData data = new PlayerData
                    {
                        name = worksheet.Cells[i, 1].Value?.ToString() ?? "",
                        level = int.TryParse(worksheet.Cells[i, 2].Value?.ToString(), out int lvl) ? lvl : 0,
                        health = int.TryParse(worksheet.Cells[i, 3].Value?.ToString(), out int hp) ? hp : 0
                    };
                    playerDataList.Add(data);
                }
            }
        }
    }

    public void WriteExcelData()
    {
        if (playerDataList == null)
        {
            Debug.LogError("playerDataList 未初始化！");
            return;
        }

        string fullPath = Path.Combine(Application.persistentDataPath, excelPath);
        FileInfo fileInfo = new FileInfo(fullPath);

        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["PlayerData"];
            if (worksheet == null)
            {
                worksheet = package.Workbook.Worksheets.Add("PlayerData");
            }

            worksheet.Cells[1, 1].Value = "名字";
            worksheet.Cells[1, 2].Value = "等级";
            worksheet.Cells[1, 3].Value = "生命值";

            for (int i = 0; i < playerDataList.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = playerDataList[i].name;
                worksheet.Cells[i + 2, 2].Value = playerDataList[i].level;
                worksheet.Cells[i + 2, 3].Value = playerDataList[i].health;
            }
            package.Save();
        }
    }

    public void UpdateExcelData()
    {
        foreach(var data in playerDataList)
        {
            data.level += 1; // 模拟等级提升
        }

        WriteExcelData();
    }
}

[System.Serializable]
class PlayerData
{
    public string name;
    public int level;
    public int health;
}