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
            NodeEditorWindow.InitEditorWindow();
        }
    }
}
