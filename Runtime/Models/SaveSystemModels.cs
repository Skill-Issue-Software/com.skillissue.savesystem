using System;
using UnityEngine;

namespace SkillIssue.SaveSystem.Models
{
    public struct AttributeQuerySettings
    {
        public Type FilterType;
        public UniquenessConstraints UniquenessConstraints;

        public AttributeQuerySettings(Type filterType = null, UniquenessConstraints uniquenessConstraints = UniquenessConstraints.None)
        {
            FilterType = filterType;
            UniquenessConstraints = uniquenessConstraints;
        }
    }
    
    [CreateAssetMenu(fileName = "SaveSystemSettings", menuName = "Skill Issue Software/Save System/Settings")]
    public class SaveSystemSettings : ScriptableObject
    {
        public StorageProvider provider = StorageProvider.Local;
        public ScriptableObject providerConfig;
    }
    
    [Serializable]
    public class SaveModuleData
    {
        public Guid guid;
        public string fullName;
        public byte[] data;
    }
    
    public interface IAttribute {}
    
    [Flags]
    public enum UniquenessConstraints : byte
    {
        None = 0,
        AttributeType = 1,
        AttributeValue = 1 << 1,
        All = AttributeType | AttributeValue
    }
    
    public enum DataFormat : byte
    {
        Bytes = 0,
        Json = 1
    }
    
    public enum SaveFilter : byte
    {
        All = 0,
        Tagged = 1,
        Custom = 2
    }

    public enum StorageProvider
    {
        Local = 0,
        AmazonS3 = 1,
        GoogleCloudStorage = 2,
        AzureBlobStorage = 3
    }
}
