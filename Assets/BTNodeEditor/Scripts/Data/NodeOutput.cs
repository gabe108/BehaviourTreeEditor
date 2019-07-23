using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    [System.Serializable]
    public class NodeOutput
    {
        public bool m_isOccupied = false;
        public BaseNode m_inputNode;

        public void UpdateGUI(BaseNode _node, NodeEditorWindow _curWindow)
        {
            Rect nodeRect = _node.GetNodeRect();
            Rect tmp = new Rect();
            tmp.width = 10f;
            tmp.height = 10f;

            float offset = 5f;
            float centerOfNode = nodeRect.x + (nodeRect.width / 2);
            float centerOfOutput = tmp.width / 2;
            float totalWidthOfOutputs = (_node.m_outputs.Count - 1) * (tmp.width + offset);
            int indexOfThis = _node.m_outputs.IndexOf(this);

            tmp.x = (centerOfNode - centerOfOutput) -
                    (totalWidthOfOutputs / 2) +
                    ((tmp.width + offset) * indexOfThis);

            tmp.y = nodeRect.y + (nodeRect.height + offset);
            if (GUI.Button(tmp, new GUIContent("")))
            {
                Debug.Log("Output " + _node.m_outputs.IndexOf(this) + " Clicked");

                if(_curWindow.GetIsMakingConnection() && _curWindow.GetConnectionType() == ConnectionType.INPUT)
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