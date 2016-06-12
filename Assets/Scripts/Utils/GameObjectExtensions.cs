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
	}
}