using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTNE
{
    [System.Serializable]
    public class RootNode : BaseNode
    {
        #region Variables

        #endregion

        #region GettersAndSetters
        #endregion

        public override void InitNode()
        {
            m_nodeName = "Root Node";
            m_nodeType = NodeType.ROOT_NODE;
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


#if UNITY_EDITOR
		public override void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            base.UpdateNodeGUI(_e, _viewRect, _skin);
        }
#endif
	}
}
