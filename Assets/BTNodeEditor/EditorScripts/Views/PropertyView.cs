﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{
#if UNITY_EDITOR
	[System.Serializable]
    public class PropertyView : ViewBase
    {
        public PropertyView() : base("Property View")
        {

        }

        public override void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e, NodeGraph _nodeGraph)
        {
            base.UpdateView(_editorRect, _percentageRect, _e, _nodeGraph);

            GUILayout.BeginArea(m_viewRect);
            GUILayout.Space(40);
            if (_nodeGraph != null)
            {
                BaseNode node = _nodeGraph.GetSelectedNode();
                if (node != null)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Name");
                    EditorGUILayout.LabelField(node.GetNodeName());

                    GUILayout.Space(10);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Node Type");
                    EditorGUILayout.LabelField(node.GetNodeType().ToString());

                    GUILayout.Space(10);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Current State");
                    EditorGUILayout.LabelField(node.GetNodeState().ToString());

                    GUILayout.Space(10);
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);

                    node.m_details = (string)EditorGUILayout.TextField(
                        new GUIContent("Details"), node.m_details);

                    GUILayout.Space(10);
                    GUILayout.EndHorizontal();

                    if (node.GetNodeType() == NodeType.ACTION_NODE)
					{
						ActionNode actionNode = ((ActionNode)node);

						GUILayout.BeginHorizontal();
						GUILayout.Space(10);

                        actionNode.m_actionType = (ActionType)EditorGUILayout.EnumPopup(
                            new GUIContent("Action Type"), actionNode.m_actionType);

						GUILayout.Space(10);
						GUILayout.EndHorizontal();

						switch (actionNode.m_actionType)
						{
							case ActionType.WALK:
								GUILayout.BeginHorizontal();
								GUILayout.Space(10);

								actionNode.m_moveToPos = (Vector3)EditorGUILayout.Vector3Field(
									"", actionNode.m_moveToPos, GUILayout.MaxWidth(1000)); 

								GUILayout.Space(10);
								GUILayout.EndHorizontal();
								break;
							case ActionType.PICKUPKEY:
								break;
							case ActionType.OPENDOOR:
								break;
							case ActionType.RESETVARIABLES:
								break;
							case ActionType.COUNT:
								break;
							default:
								break;
						}
					}
                }
            }
            GUILayout.EndArea();
            ProcessEvents(_e);
        }

        public override void ProcessEvents(Event _e)
        {
            base.ProcessEvents(_e);

            if (GetViewRect().Contains(_e.mousePosition))
            {

            }
        }
    }
#endif
}