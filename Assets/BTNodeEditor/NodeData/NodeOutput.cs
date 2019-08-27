using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    [System.Serializable]
    public class NodeOutput : BaseNodeIO
    {
        public NodeOutput()
        {
            m_type = ConnectionType.OUTPUT;
        }

#if UNITY_EDITOR
		public override void UpdateGUI(BaseNode _node, NodeEditorWindow _curWindow)
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
            m_IORect = tmp;

            base.UpdateGUI(_node, _curWindow);

            if (m_connectedTo != null && m_isOccupied)
            {
                UnityEditor.Handles.DrawLine(m_IORect.center, m_connectedTo.m_IORect.center);
            }
        }
#endif
	}
}