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

		/// <summary>
		/// Determines whether the current object is a node or not.
		/// </summary>
		public static bool IsGraphNode(this GameObject gameObject)
		{
			return gameObject.CompareTag(Tags.Node);
		}

		/// <summary>
		/// Displays the label on a node and returns its original font size so it can be restored later on.
		/// </summary>
		public static int DisplayNodeLabel(this GameObject node, int fontSize)
		{
			var label = node.transform.FindChild(Constants.NodeLabelGameObjectName).gameObject;
			label.SetActive(true);

			var textMesh = label.GetComponent<TextMesh>();
			var originaFontSize = textMesh.fontSize;
			textMesh.fontSize = fontSize;

			return originaFontSize;
		}

		/// <summary>
		/// Hides the label on a node and restore its original font size.
		/// </summary>
		public static void HideNodeLabel(this GameObject node, int originalFontSize)
		{
			var label = node.transform.FindChild(Constants.NodeLabelGameObjectName).gameObject;
			label.SetActive(false);
			label.GetComponent<TextMesh>().fontSize = originalFontSize;
		}

		/// <summary>
		/// Restores the original font size of a node label
		/// </summary>
		public static void RestoreOriginalNodeLabelSize(this GameObject node, int originalFontSize)
		{
			var label = node.transform.FindChild(Constants.NodeLabelGameObjectName).gameObject;
			label.GetComponent<TextMesh>().fontSize = originalFontSize;
		}
	}
}