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

		public static void SetSize(this GameObject gameObject, float size)
		{
			gameObject.transform.localScale = new Vector3(size, size, size);
		}

		public static void SetColor(this GameObject gameObject, Color color)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = color;
		}

		public static bool IsGraphNode(this GameObject gameObject)
		{
			return
				gameObject.CompareTag("node") ||
				gameObject.CompareTag(NodeType.Organisation.ToString()) ||
				gameObject.CompareTag(NodeType.Product.ToString()) ||
				gameObject.CompareTag(NodeType.Person.ToString());
		}

		public static int DisplayNodeLabel(this GameObject node, int fontSize)
		{
			var label = node.transform.FindChild("label").gameObject;
			label.SetActive(true);

			var textMesh = label.GetComponent<TextMesh>();
			var originaFontSize = textMesh.fontSize;
			textMesh.fontSize = fontSize;

			return originaFontSize;
		}

		public static void HideNodeLabel(this GameObject node, int originalFontSize)
		{
			var label = node.transform.FindChild("label").gameObject;
			label.SetActive(false);
			label.GetComponent<TextMesh>().fontSize = originalFontSize;
		}

		public static void RestoreOriginalNodeLabelSize(this GameObject node, int originalFontSize)
		{
			var label = node.transform.FindChild("label").gameObject;
			label.GetComponent<TextMesh>().fontSize = originalFontSize;
		}
	}
}