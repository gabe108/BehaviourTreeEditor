using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTNE
{
    public enum ActionType
    {
        WALK,
		PICKUPKEY,
		OPENDOOR,
		RESETVARIABLES,

        COUNT,
	}

    public class ActionNode : BaseNode
    {
        #region Variables
        public ActionType m_actionType;
		public GameObject m_object;
		#endregion

		#region GettersAndSetters
		#endregion

		private void OnEnable()
		{
			m_actionCompleted = false;
		}

		public override void InitNode()
        {
            m_nodeName = "Action Node";
            m_nodeType = NodeType.ACTION_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
            
            base.InitNode();
        }

        public override NodeStates Evaluate()
        {
            mDebug();
            switch (m_actionType)
            {
                case ActionType.WALK:
                    m_nodeState = Walk();
                    break;

                case ActionType.PICKUPKEY:
					m_nodeState = PickUpKey();
					break;

				case ActionType.OPENDOOR:
					m_nodeState = OpenDoor();
					break;

				case ActionType.RESETVARIABLES:
					m_nodeState = ResetVariables();
					break;

			}

            switch (m_nodeState)
            {
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                default:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
            }
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
		NodeStates Walk()
        {
			//Debug.Break();
			if (m_actionCompleted)
				return NodeStates.SUCCESS;

			Vector3 p1 = m_player.transform.position;
			Vector3 p2 = m_object.transform.position;

			m_player.transform.position = Vector3.MoveTowards(p1, p2, 10.0f * Time.deltaTime);

			if (p1 == p2)
			{
				m_actionCompleted = true;
				return NodeStates.SUCCESS;
			}
			else
				return NodeStates.RUNNING;
		}

		NodeStates PickUpKey()
		{
			if (m_actionCompleted)
				return NodeStates.SUCCESS;

			m_player.m_hasKey = true;
			m_actionCompleted = true;
			return NodeStates.SUCCESS;
		}		

		NodeStates OpenDoor()
		{
			if (m_actionCompleted)
				return NodeStates.SUCCESS;

			if (m_player.m_hasKey)
			{
				m_actionCompleted = true;
				return NodeStates.SUCCESS;
			}
			else
			{
				return NodeStates.FAILURE;
			}
		}

		NodeStates ResetVariables()
		{
			foreach (BaseNode node in m_parentGraph.m_nodes)
				node.m_actionCompleted = false;

			return NodeStates.SUCCESS;
		}
	}
}