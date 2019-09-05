using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    public class InverterNode : BaseNode
    {
		#region Variables

		#endregion

		#region GettersAndSetters
		#endregion

#if UNITY_EDITOR
		public override void InitNode()
        {
            m_nodeName = "Inverter";
            m_nodeType = NodeType.INVERTER_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
            base.InitNode();
        }
#endif

        public override NodeStates Evaluate()
        {
            mDebug();

            if (m_outputs[0].m_connectedTo != null)
            {
                BaseNode node = m_outputs[0].m_connectedTo.m_holderNode;
                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        m_nodeState = NodeStates.SUCCESS;
                        return m_nodeState;
                    case NodeStates.SUCCESS:
                        m_nodeState = NodeStates.FAILURE;
                        return m_nodeState;
                    case NodeStates.RUNNING:
                        m_nodeState = NodeStates.RUNNING;
                        return m_nodeState;
                }
            }
            m_nodeState = NodeStates.SUCCESS;
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