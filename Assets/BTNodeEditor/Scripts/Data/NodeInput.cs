using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    [System.Serializable]
    public class NodeInput
    {
        public bool m_isOccupied = false;

        public void UpdateGUI(BaseNode _node, NodeEditorWindow _curWindow)
        {
            if (_node.GetNodeType() != NodeType.ROOT_NODE)
            {
                Rect nodeRect = _node.GetNodeRect();
                Rect tmp = new Rect();
                tmp.width = 13f;
                tmp.height = 13f;

                float offset = 5f;
                float centerOfNode = nodeRect.x + (nodeRect.width / 2);
                float centerOfOutput = tmp.width / 2;

                tmp.x = centerOfNode - centerOfOutput;
                tmp.y = nodeRect.y - (tmp.height + offset);
                if (GUI.Button(tmp, new GUIContent("")))
                {
                    Debug.Log("Input Clicked");

                    if (_curWindow.GetIsMakingConnection() && _curWindow.GetConnectionType() == ConnectionType.OUTPUT)
                    {

                    }
                    else if (!_curWindow.GetIsMakingConnection())
                    {
                        //_curWindow.SetIsMakingConnection(true);
                        _curWindow.SetConnectionType(ConnectionType.OUTPUT);
                        _curWindow.SetConnectionStart(tmp.center);
                    }
                }
            }
        }
    }
}
