using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    public class SelectorNode : BaseNode
    {
		#region Variables

		#endregion

		#region GettersAndSetters
		#endregion

#if UNITY_EDITOR
		public override void InitNode()
        {
            m_nodeName = "Selector";
            m_nodeType = NodeType.SELECTOR_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
            base.InitNode();
        }
#endif
		public override NodeStates Evaluate()
        {
            mDebug();

            foreach (NodeOutput output in m_outputs)
            {
                if (output == null)
                    continue;

                if (output.m_connectedTo == null)
                    continue;

                BaseNode node = output.m_connectedTo.m_holderNode;

                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        continue;
                    case NodeStates.SUCCESS:
                        m_nodeState = NodeStates.SUCCESS;
                        return m_nodeState;
                    case NodeStates.RUNNING:
                        m_nodeState = NodeStates.RUNNING;
                        return m_nodeState;
                    default:
                        continue;
                }
            }
            m_nodeState = NodeStates.FAILURE;
            return m_nodeState;
        }

        public override void UpdateNode(Event _e, Rect _viewRect)
        {
            base.UpdateNode(_e, _viewRect);
        }

#if UNITY_EDITOR
		public override void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            base.UpdateNodeGUI(_e, _viewRect, _skin);
        }
#endif
	}
}