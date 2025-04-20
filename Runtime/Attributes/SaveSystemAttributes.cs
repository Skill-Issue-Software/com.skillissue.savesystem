using System;
using SkillIssue.SaveSystem.Models;

namespace SkillIssue.SaveSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SaveClassData : Attribute, IAttribute
    {
        public readonly DataFormat Format;
        
        public SaveClassData(DataFormat format = DataFormat.Bytes)
        {
            Format = format;
        }
    }
}
