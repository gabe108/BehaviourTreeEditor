using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    [System.Serializable]
    public class NodeInput : BaseNodeIO
    {
        public NodeInput()
        {
            m_type = ConnectionType.INPUT;
        }

        public override void UpdateGUI(BaseNode _node, NodeEditorWindow _curWindow)
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
                m_IORect = tmp;

                base.UpdateGUI(_node, _curWindow);
            }
        }
    }
}
