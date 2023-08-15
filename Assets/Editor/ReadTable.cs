using UnityEngine;
using UnityEditor;
using OfficeOpenXml;
using System.IO;
using System;
using System.Reflection;

[InitializeOnLoad]
public  class Startup
{
    //在运行前执行此方法
    static Startup()
    {
        string path =Application.dataPath + "/Editor/关卡管理.xlsx";
        string assetName = "Level"; //表示我们导出完数据后存储的资源名称

        FileInfo fileInfo = new FileInfo(path);
        //创建序列化类
        LevelData levelData =(LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
        using(ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["僵尸"];
            
            //遍历每一行，循环执行
            for (int i = worksheet.Dimension.Start.Row + 2; i<= worksheet.Dimension.End.Row;i++)
            {
                LevelItem levelItem = new LevelItem();
                //获取LevelItem的Type
                Type type = typeof(LevelItem);

                //遍历每一列
                for(int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                {
                    //反射机制


                    //打印日志输出数据
                    Debug.Log("数据内容："+ worksheet.GetValue(i,j).ToString());
                    FieldInfo variable = type.GetField(worksheet.GetValue(2,j).ToString());
                    string tableValue = worksheet.GetValue(i,j).ToString();
                    variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));

                }
                //当前行赋值结束，添加到列表中
                levelData.LevelDataList.Add(levelItem);
            }

        }
        //保存ScriptableObject为.asset文件
        AssetDatabase.CreateAsset(levelData,"Assets/Resources/" + assetName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

}