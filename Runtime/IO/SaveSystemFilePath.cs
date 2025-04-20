using System;
using UnityEngine;

namespace SkillIssue.SaveSystem.IO
{
    public static class SaveSystemFilePath
    {
        private static readonly string _base = Application.persistentDataPath;
        
        public static string Default => $"{_base}/Default.sissf";
        
        public static string GetSaveSlotWithName(string path)
            => $"{_base}/{path}.sissf";
    }
}
