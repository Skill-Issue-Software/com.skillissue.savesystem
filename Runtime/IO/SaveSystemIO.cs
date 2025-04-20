using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SkillIssue.SaveSystem.Models;
using UnityEngine;

namespace SkillIssue.SaveSystem.IO
{
    public static class SaveSystemIO
    {
        public static void SaveDataToFile(List<SaveModuleData> modules)
        {
            SaveDataToFile(modules, SaveSystemFilePath.Default);
        }
        
        public static void SaveDataToFile(List<SaveModuleData> modules, string path)
        {
            using FileStream fileStream = new(path, FileMode.Create);
            using BinaryWriter binaryWriter = new(fileStream);
            
            binaryWriter.Write(XorCypher(Encoding.UTF8.GetBytes("sissf")));
            binaryWriter.Write((byte)1); // TODO: Move hard-coded version to config file later
            
            modules.ForEach(module => WriteModule(binaryWriter, module));

            Debug.Log($"Saved data to path {path}");
        }

        private static void WriteModule(BinaryWriter binaryWriter, SaveModuleData module)
        {
            WriteInSissfFormat(binaryWriter, module.guid.ToByteArray());
            WriteInSissfFormat(binaryWriter, Encoding.UTF8.GetBytes(module.fullName));
            WriteInSissfFormat(binaryWriter, module.data);
        }
        
        private static void WriteInSissfFormat(BinaryWriter binaryWriter, byte[] data)
        {
            binaryWriter.Write(data.Length);
            binaryWriter.Write(XorCypher(data));
        }
        
        public static byte[] XorCypher(byte[] data, byte key = 0x5a) // TODO: Move hard-coded key to config file later
            => data.Select(entry => (byte)(entry ^ key)).ToArray();
        
        public static List<SaveModuleData> LoadDataFromFile()
        {
            return LoadDataFromFile(SaveSystemFilePath.Default);
        }
        
        public static List<SaveModuleData> LoadDataFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"Could not find save file at path {path}");
                return new List<SaveModuleData>();
            }
            
            using FileStream fileStream = new(path, FileMode.Open);
            using BinaryReader binaryReader = new(fileStream);
            
            string signature = Encoding.UTF8.GetString(XorCypher(binaryReader.ReadBytes("sissf".Length)));
            
            if (signature != "sissf")
            {
                Debug.LogError($"Invalid file signature: {signature}. Expected: \"sissf\".");
                return new List<SaveModuleData>();
            }
            
            byte version = binaryReader.ReadByte();

            if (version != 1) // TODO: Don't hard-code, move to config file later
            {
                Debug.LogError($"Unknown sissf version: {version}");
                return new List<SaveModuleData>();
            }
            
            List<SaveModuleData> modules = new();
            
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                Guid guid = new(ReadChunk(binaryReader));
                string fullName = Encoding.UTF8.GetString(ReadChunk(binaryReader));
                byte[] data = ReadChunk(binaryReader);

                modules.Add(new SaveModuleData
                {
                    guid = guid,
                    fullName = fullName,
                    data = data
                });
            }
            
            return modules;
        }
        
        private static byte[] ReadChunk(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            byte[] encrypted = reader.ReadBytes(length);
            return XorCypher(encrypted);
        }
        
        private static byte[] ReadInSissfFormat(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            return reader.ReadBytes(length);
        }
    }
}
