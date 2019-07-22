using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BTNE
{
    public static class NodeUtils 
    {
        public static void CreateGraph(string _wantedName, string _path)
        {
            NodeGraph curGraph = ScriptableObject.CreateInstance<NodeGraph>() as NodeGraph;
            if(curGraph != null)
            {
                curGraph.SetGraphName(_wantedName);
                curGraph.InitGraph();

                if (string.IsNullOrEmpty(_path))
                    _path = "Assets/BTNodeEditor/Database";

                AssetDatabase.CreateAsset(curGraph, _path + "/" + curGraph.GetGraphName() + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
                if(curWindow != null)
                {
                    curWindow.SetCurrentGraph(curGraph);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error Something Worng", "Wasnt able to the set current graph variable for the NodeEditorWindow", "Ok");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error Something Worng", "Wasnt able to create a new graph. See ur friendly programmer", "Ok");
            }
        }

        public static void LoadGraph()
        {
            NodeGraph curGraph = null;
            string pathToLoad = EditorUtility.OpenFilePanel("Load BehaviourTree", "Asset/", "");

            int appPathLen = Application.dataPath.Length;
            string fullPath = pathToLoad.Substring(appPathLen - 6);

            curGraph = AssetDatabase.LoadAssetAtPath(fullPath, typeof(NodeGraph)) as NodeGraph;
            if(curGraph != null)
            {
                NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
                if (curWindow != null)
                {
                    curWindow.SetCurrentGraph(curGraph);
                }
            }
        }

        public static void UnloadGraph()
        {
            NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            if (curWindow != null)
            {
                curWindow.SetCurrentGraph(null);
            }
            else
            {
                EditorUtility.DisplayDialog("Error Something Worng", "Something went wrong in getting the current window", "Ok");
            }
        }

        public static void CreateNode(NodeGraph _currGraph, NodeType _nodeType, Vector2 mousePos)
        {
            if(_currGraph != null)
            {
                BaseNode curNode = null;
                switch (_nodeType)
                {
                    case NodeType.ADD_NODE:
                        curNode = ScriptableObject.CreateInstance<AddNode>() as AddNode;
                        break;

                    default:
                        break; 
                }

                if(curNode != null)
                {
                    curNode.InitNode();
                    curNode.SetNodeRect(    
                        (int)mousePos.x, 
                        (int)mousePos.y, 
                        (int)curNode.GetNodeRect().width, 
                        (int)curNode.GetNodeRect().height);
                    curNode.SetParentGraph(_currGraph);
                    _currGraph.m_nodes.Add(curNode);
                }
            }
        }
    }
}