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

        public override void InitNode()
        {
            m_nodeName = "Inverter";
            m_nodeType = NodeType.INVERTER_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
            base.InitNode();
        }

        public override NodeStates Evaluate()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateNode(Event _e, Rect _viewRect)
        {
            base.UpdateNode(_e, _viewRect);
        }

        public override void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            base.UpdateNodeGUI(_e, _viewRect, _skin);
        }
    }
}