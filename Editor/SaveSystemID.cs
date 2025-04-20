using System;
using UnityEditor;
using UnityEngine;

namespace SkillIssue.SaveSystem.Editor
{
    [ExecuteAlways]
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-99999)]
    public class SaveSystemID : MonoBehaviour
    {
        // TODO: Enable HideInInspector for production builds
        [SerializeField] /*[HideInInspector]*/ private string guid;

        public byte[] GuidAsBytes => Guid.TryParse(guid, out Guid id) ? id.ToByteArray() : null;
        
#if UNITY_EDITOR
        private void Awake()
        {
            // TODO: Enable hideFlags for production builds
            
            // hideFlags = HideFlags.HideInInspector;
            
            if (!string.IsNullOrWhiteSpace(guid) || Guid.TryParse(guid, out _)) return;
            
            guid = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
    
#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class SissfFiletypeIconSetter
    {
        private const string CustomFileExtension = ".sissf";
        private const string IconPath = "Packages/com.skillissue.savesystem/Editor/Icons/SkillIssueSoftwareIcon.png";

        static SissfFiletypeIconSetter()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            
            if (!path.EndsWith(CustomFileExtension, StringComparison.OrdinalIgnoreCase)) return;
            
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);
            
            if (icon == null) return;
            
            bool isOneColumn = selectionRect.height <= 20f;
            float iconSize = isOneColumn ? 16f : selectionRect.height * 0.8f;

            float x = isOneColumn
                ? selectionRect.x + 2f
                : selectionRect.x + (selectionRect.width - iconSize) * 0.5f;

            float y = isOneColumn
                ? selectionRect.y
                : selectionRect.y - iconSize * 0.05f;

            GUI.DrawTexture(new Rect(x, y, iconSize, iconSize), icon, ScaleMode.ScaleToFit);
        }
    }
#endif
}
