using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{
#if UNITY_EDITOR
	public class NodePopupWindow : EditorWindow
    {
        #region Variables
        private static NodePopupWindow m_currPopup;
        private string m_wantedName = "Enter a name..";
        private string m_wantedPath;
        #endregion

        #region GettersAndSetters
        public string GetWantedName() { return m_wantedName; }
        #endregion

        public static void InitPopupWindow()
        {
            m_currPopup = GetWindow<NodePopupWindow>() as NodePopupWindow;
            m_currPopup.titleContent = new GUIContent("Create a new Graph");
            m_currPopup.minSize = new Vector2(500, 500);
        }

        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);

            GUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.LabelField(new GUIContent("Create a sNew Graph:"));
            m_wantedName = EditorGUILayout.TextField("Enter graph name", m_wantedName, GUILayout.Height(22));
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Path"));
            EditorGUILayout.LabelField(new GUIContent(m_wantedPath));
            if (GUILayout.Button("Browse", GUILayout.Height(20)))
            {
                m_wantedPath = EditorUtility.SaveFolderPanel("Browse", "Asset/", "");
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Create Graph", GUILayout.Height(40)))
            {
                if(!string.IsNullOrEmpty(m_wantedName) && m_wantedName != "Enter a name..")
                {
                    if (m_wantedPath.Contains("Assets"))
                    {
                        int appPathLen = Application.dataPath.Length;
                        string fullPath = m_wantedPath.Substring(appPathLen - 6);

                        NodeUtils.CreateGraph(m_wantedName, fullPath);

                        if (m_currPopup != null)
                            m_currPopup.Close();
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Warning", "Please select a folder inside Assets folder", "ok");
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Warning", "Enter a valid name", "ok");
                }
            }

            if(GUILayout.Button("Cancel", GUILayout.Height(40)))
            {
                if (m_currPopup != null)
                    m_currPopup.Close();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.EndVertical();

            GUILayout.Space(20);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            Repaint();
        }
    }
#endif
}