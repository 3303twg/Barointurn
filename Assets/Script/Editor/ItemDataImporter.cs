using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDataImporter : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Import Item Stats From JSON")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ItemDataImporter), false, "ItemStat Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("ItemStat JSON Importer", EditorStyles.boldLabel);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField("JSON File", jsonFile, typeof(TextAsset), false);

        if (GUILayout.Button("Import"))
        {
            if (jsonFile == null)
            {
                EditorUtility.DisplayDialog("Error", "JSON ������ �������ּ���!", "Ȯ��");
                return;
            }

            ImportJson(jsonFile);
        }
    }

    private void ImportJson(TextAsset json)
    {
        ItemDataWrapper wrapper = JsonUtility.FromJson<ItemDataWrapper>(json.text);
        if (wrapper == null || wrapper.Item == null)
        {
            Debug.LogError("JSON �Ľ� ����!");
            return;
        }

        // ���ø� SO �ڵ� �ε�
        string templatePath = "Assets/Data/So/ItemData/ItemTemplates";
        string[] guids = AssetDatabase.FindAssets("t:ItemTemplate_So", new[] { templatePath });
        List<ItemTemplate_So> templateSOList = new List<ItemTemplate_So>();
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemTemplate_So templateSO = AssetDatabase.LoadAssetAtPath<ItemTemplate_So>(path);
            if (templateSO != null)
                templateSOList.Add(templateSO);
        }

        // SO ���� ����
        string folderPath = "Assets/Data/So/ItemData";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Data/So", "ItemData");
        }

        foreach (var data in wrapper.Item)
        {
            // int�� ItemID�� ���ø� SO ã��
            ItemTemplate_So templateSO = templateSOList.Find(x => x.ItemID == data.ItemID);
            if (templateSO == null)
            {
                Debug.LogWarning($"���ø� SO ����: {data.ItemID}");
                Logger.Log("����?");
                continue;
            }

            // ItemData_So ����
            ItemData_So soAsset = ScriptableObject.CreateInstance<ItemData_So>();
            soAsset.ItemID = data.ItemID;
            if (templateSO != null)
            {
                soAsset.template = templateSO; // ���ø� ���� ����
                soAsset.prefab = templateSO.prefab;
                soAsset.sprite = templateSO.sprite;
            }
            soAsset.Name = data.Name;
            soAsset.Description = data.Description;
            soAsset.UnlockLev = data.UnlockLev;
            soAsset.MaxHP = data.MaxHP;
            soAsset.MaxHPMul = data.MaxHPMul;
            soAsset.MaxMP = data.MaxMP;
            soAsset.MaxMPMul = data.MaxMPMul;
            soAsset.MaxAtk = data.MaxAtk;
            soAsset.MaxAtkMul = data.MaxAtkMul;
            soAsset.MaxDef = data.MaxDef;
            soAsset.MaxDefMul = data.MaxDefMul;

            string assetPath = $"{folderPath}/{soAsset.ItemID}.asset";
            AssetDatabase.CreateAsset(soAsset, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("�Ϸ�", "ItemStat SO ���� ���� �Ϸ�!", "Ȯ��");
    }
}
