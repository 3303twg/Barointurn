using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

public class EnemyDataImporter : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Import Enemy Stats From JSON")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataImporter>("EnemyStat Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("EnemyStat JSON Importer", EditorStyles.boldLabel);

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
        if (json == null || string.IsNullOrEmpty(json.text))
        {
            Debug.LogError("JSON ������ ����ְų� ��ȿ���� �ʽ��ϴ�.");
            return;
        }

        JObject root;
        try
        {
            root = JObject.Parse(json.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON �Ľ� ����: {e.Message}");
            return;
        }

        JArray monsters = root["Monster"] as JArray;
        if (monsters == null)
        {
            Debug.LogError("Monster �迭�� ã�� �� �����ϴ�.");
            return;
        }

        // EnemyTemplate_So �ε�
        string templatePath = "Assets/Data/So/EnemyData/EnemyTemplates";
        string[] templateGuids = AssetDatabase.FindAssets("t:EnemyTemplate_So", new[] { templatePath });
        List<EnemyTemplate_So> templateSOList = new List<EnemyTemplate_So>();
        foreach (var guid in templateGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            EnemyTemplate_So templateSO = AssetDatabase.LoadAssetAtPath<EnemyTemplate_So>(path);
            if (templateSO != null) templateSOList.Add(templateSO);
        }

        // ItemData_So �ε�
        string itemSoPath = "Assets/Data/So/ItemData";
        string[] itemGuids = AssetDatabase.FindAssets("t:ItemData_So", new[] { itemSoPath });
        List<ItemData_So> allItemSO = new List<ItemData_So>();
        foreach (var guid in itemGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData_So itemSO = AssetDatabase.LoadAssetAtPath<ItemData_So>(path);
            if (itemSO != null) allItemSO.Add(itemSO);
        }

        // EnemyStat_SO ���� ���� Ȯ��
        string folderPath = "Assets/Data/So/EnemyData";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Data/So", "EnemyData");

        // ���� CollectData_So�� SpawnData_SO �ε�
        CollectData_So collectData = AssetDatabase.LoadAssetAtPath<CollectData_So>("Assets/Data/So/CollectData/CollectData_So.asset");
        collectData.enemyData = new List<EnemyStat_So>();

        EnemySpawnData_SO spawnData = AssetDatabase.LoadAssetAtPath<EnemySpawnData_SO>("Assets/Data/So/SpawnData/SpawnData.asset");
        spawnData.EnemySoList = new List<EnemyStat_So>();


        foreach (JObject monster in monsters)
        {
            EnemyStat_So soAsset = ScriptableObject.CreateInstance<EnemyStat_So>();
            soAsset.MonsterID = monster.Value<string>("MonsterID");
            soAsset.Name = monster.Value<string>("Name");
            soAsset.Description = monster.Value<string>("Description");
            soAsset.Attack = monster.Value<float>("Attack");
            soAsset.AttackMul = monster.Value<float>("AttackMul");
            soAsset.MaxHP = monster.Value<float>("MaxHP");
            soAsset.MaxHPMul = monster.Value<float>("MaxHPMul");
            soAsset.AttackRange = monster.Value<float>("AttackRange");
            soAsset.AttackRangeMul = monster.Value<float>("AttackRangeMul");
            soAsset.AttackSpeed = monster.Value<float>("AttackSpeed");
            soAsset.MoveSpeed = monster.Value<float>("MoveSpeed");
            soAsset.MinExp = monster.Value<int>("MinExp");
            soAsset.MaxExp = monster.Value<int>("MaxExp");

            // EnemyTemplate ����
            EnemyTemplate_So templateSO = templateSOList.Find(x => x.MonsterID == soAsset.MonsterID);
            if (templateSO != null)
            {
                soAsset.template = templateSO;
                soAsset.prefab = templateSO.prefab;
                soAsset.sprite = templateSO.sprite;
            }
            else
            {
                Debug.LogWarning($"���ø� SO ����: {soAsset.MonsterID}");
            }

            // DropItem ó��
            soAsset.DropItem.Clear();
            JToken dropToken = monster["DropItem"];
            List<int> dropIDs = new List<int>();

            if (dropToken != null)
            {
                if (dropToken.Type == JTokenType.String)
                {
                    string dropStr = dropToken.Value<string>();
                    dropIDs.AddRange(dropStr.Split(',')
                                             .Select(s => int.TryParse(s.Trim(), out int id) ? id : -1)
                                             .Where(id => id != -1));
                }
                else if (dropToken.Type == JTokenType.Integer)
                {
                    dropIDs.Add(dropToken.Value<int>());
                }
            }

            foreach (var dropID in dropIDs)
            {
                ItemData_So matchItem = allItemSO.Find(x => x.ItemID == dropID);
                if (matchItem != null && matchItem.prefab != null)
                {
                    soAsset.DropItem.Add(matchItem.prefab);
                }
                else
                {
                    Debug.LogWarning($"DropItem SO ����: {dropID} (���� {soAsset.MonsterID})");
                }
            }

            // SO ���� ����
            string assetPath = $"{folderPath}/{soAsset.MonsterID}.asset";
            AssetDatabase.CreateAsset(soAsset, assetPath);

            // ���� SO ����Ʈ�� �߰�
            if (collectData != null && !collectData.enemyData.Contains(soAsset))
                collectData.enemyData.Add(soAsset);

            if (spawnData != null && !spawnData.EnemySoList.Contains(soAsset))
                spawnData.EnemySoList.Add(soAsset);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("�Ϸ�", "EnemyStat SO ���� ���� �� ����Ʈ ������Ʈ �Ϸ�!", "Ȯ��");
    }
}
