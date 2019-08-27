using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    public class SequenceNode : BaseNode
    {
        #region Variables

        #endregion

        #region GettersAndSetters
        #endregion

        public override void InitNode()
        {
            m_nodeName = "Sequence";
            m_nodeType = NodeType.SEQUENCE_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
            base.InitNode();
        }

        public override NodeStates Evaluate()
        {
            mDebug();
            bool anyChildRunning = false;

            foreach (NodeOutput output in m_outputs)
            {
                if (output.m_connectedTo == null)
                    continue;

                BaseNode node = output.m_connectedTo.m_holderNode;

                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        m_nodeState = NodeStates.FAILURE;
                        return m_nodeState;
                    case NodeStates.SUCCESS:
                        continue;
                    case NodeStates.RUNNING:
                        anyChildRunning = true;
						return m_nodeState;
					default:
                        m_nodeState = NodeStates.SUCCESS;
                        return m_nodeState;
                }
            }

            m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
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
