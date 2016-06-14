using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public static class GameObjectExtensions
	{
		public static void SetPosition(this GameObject gameObject, Vector3 newPosition)
		{
			gameObject.transform.position = newPosition;
		}

		public static void SetParent(this GameObject gameObject, GameObject parent)
		{
			gameObject.transform.parent = parent.transform;
		}

	    public static bool IsGraphNode(this GameObject gameObject)
	    {
	        return gameObject.CompareTag(NodeType.Organisation.ToString()) ||
	               gameObject.CompareTag(NodeType.Product.ToString()) ||
	               gameObject.CompareTag(NodeType.Person.ToString());
	    }
	}
}