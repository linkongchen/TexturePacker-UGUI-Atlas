using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class PackageGUIWin : EditorWindow
{
    struct GUIInfo
    {
        public string In;
        public string Name;
        public string Out;

        public GUIInfo(string i,string n,string o)
        {
            In = i;
            Name = n;
            Out = o;
        }
    }


    [MenuItem("XTools/CMD/自动打包UGUI图集(+程序使用+)", false, 999)]
    public static void ExecuteOnePackageWin()
    {
        InitXML();

        PackageGUIWin window = EditorWindow.GetWindow(typeof(PackageGUIWin), false, "PackagePlotWin", false) as PackageGUIWin;
        window.maxSize = new Vector2(300, 400);
        window.Show();
    }

    #region 解析xml

    private static List<GUIInfo> InfoList;

    private static void InitXML()
    {
#pragma warning disable 0618
        WWW www = new WWW(PathManager.FileBasePath() + "/Editor/CMD/TPackageInfo.xml");
        while (!www.isDone)
        {
        }
        if (string.IsNullOrEmpty(www.error))
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(www.text);
            XmlNodeList soundBankList = XmlDoc.GetElementsByTagName("Item");
            InfoList = new List<GUIInfo>();
            foreach (XmlNode node in soundBankList)
            {
                GUIInfo info = new GUIInfo(node.Attributes["In"].Value, node.Attributes["Name"].Value, node.Attributes["Out"].Value);
                InfoList.Add(info);
            }
        }
        else
        {
            Log.Error(www.error);
        }
    }
    #endregion


    string _input_dir = "";
    string _folder_name = "";
    string _output_dir = "";

    Vector2 v2 = new Vector2(0,80);
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("美术输出路径名\n(B.背包)");
        GUILayout.Label("美术输出文件夹名\n(ui_bag)");
        GUILayout.Label("程序输入文件夹名\n(Backpack)");
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        _input_dir = GUILayout.TextField(_input_dir, GUILayout.Height(40));
        GUILayout.Space(20);
        _folder_name = GUILayout.TextField(_folder_name, GUILayout.Height(40));
        GUILayout.Space(20);
        _output_dir = GUILayout.TextField(_output_dir, GUILayout.Height(40));
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        v2 = GUILayout.BeginScrollView(v2, GUILayout.Width(300), GUILayout.Height(250));

        foreach(var item in InfoList)
        {
            if (GUILayout.Button(item.In + " " + item.Name + " " + item.Out, GUILayout.Height(30)))
            {
                _input_dir = item.In;
                _folder_name = item.Name;
                _output_dir = item.Out;
            }
        }

        GUILayout.EndScrollView();
        GUILayout.Space(5);
        if (GUILayout.Button("确定", GUILayout.Height(50)) && !string.IsNullOrEmpty(_input_dir) && !string.IsNullOrEmpty(_folder_name) && !string.IsNullOrEmpty(_output_dir))
        {
            CMDTools.ExecuteOneGUIPackage(_input_dir,_folder_name, _output_dir);
        }
        GUILayout.EndVertical();
    }
}
