using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using SkillIssue.SaveSystem.Attributes;
using SkillIssue.SaveSystem.Editor;
using SkillIssue.SaveSystem.IO;
using SkillIssue.SaveSystem.Models;

namespace SkillIssue.SaveSystem.Core
{
    public static class SaveSystemUtilities
    {
        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null) return null;
            
            GameObject instance = UnityEngine.Object.Instantiate(prefab, position, rotation);
            instance.AddComponent<SaveSystemID>();
            return instance;
        }
        
        public static GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            if (prefab == null) return null;
            
            GameObject instance = UnityEngine.Object.Instantiate(prefab, parent);
            instance.AddComponent<SaveSystemID>();
            return instance;
        }
        
        private static void TagAllGameObjects()
        {
            GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None)
                .Where(gameObject => gameObject.GetComponent<SaveSystemID>() == null)
                .ToList()
                .ForEach(gameObject => gameObject.AddComponent<SaveSystemID>());
        }
        
        public static List<Type> GetSavedTypes()
            => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttribute<SaveClassData>() != null)
                .ToList();

        public static List<object> GetSavedInstances()
            => GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .Where(monoBehaviour => Attribute.IsDefined(monoBehaviour.GetType(), typeof(SaveClassData)))
                .Cast<object>()
                .ToList();

        public static Dictionary<string, object> GetFieldsOfInstance(object instance)
            => instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .ToDictionary(field => field.Name, field => field.GetValue(instance));

        public static void ConvertAndSaveData()
        {
            TagAllGameObjects();
            
            List<SaveModuleData> data = GetSavedInstances()
                .Select(instance => new SaveModuleData
                {
                    guid = Guid.NewGuid(),
                    fullName = instance.GetType().FullName,
                    data = Encoding.UTF8.GetBytes(JsonUtility.ToJson(instance))
                }).ToList();
            
            SaveSystemIO.SaveDataToFile(data);
        }

        public static void ConvertAndLoadData()
        {
            List<object> instances = GetSavedInstances();
            
            List<SaveModuleData> modules = SaveSystemIO.LoadDataFromFile();

            foreach (SaveModuleData module in modules)
            {
                object target = instances.FirstOrDefault(instance => instance.GetType().FullName == module.fullName);
                
                if (target == null)
                {
                    Debug.LogWarning($"Could not find instance of type {module.fullName}");
                    continue;
                }
                
                string json = Encoding.UTF8.GetString(module.data);
                JsonUtility.FromJsonOverwrite(json, target);
            }
        }
    }
}
