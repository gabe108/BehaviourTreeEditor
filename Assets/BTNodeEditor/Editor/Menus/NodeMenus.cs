using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{ 
    public class NodeMenus 
    {
        [MenuItem("BTNE/Launch BT Node Editor")]
        public static void InitNodeEditor()
        {
            NodeEditorWindow m_mainWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            m_mainWindow.titleContent = new GUIContent("BTNE");

            m_mainWindow.InitEditorWindow();
        }
    }
}
