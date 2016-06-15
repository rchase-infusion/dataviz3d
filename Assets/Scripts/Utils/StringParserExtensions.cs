using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public static class StringParserExtensions
	{
		public static int ToInt(this string value)
		{
			return Int32.Parse(value);
		}

		public static Vector3 ToVector3(this string value)
		{
			var coordinates = value.Split(',');
			return new Vector3(coordinates[0].ToFloat(), coordinates[1].ToFloat(), coordinates[2].ToFloat());
		}

		public static float ToFloat(this string value)
		{
			return float.Parse(value);
		}

		public static Color ToColor(this string value)
		{
			var color = value.ToLower();

			switch (color)
			{
				case "red":
					return Color.red;
				case "green":
					return Color.green;
				case "blue":
					return Color.blue;
				case "white":
					return Color.white;
				case "black":
					return Color.black;
				case "yellow":
					return Color.yellow;
				case "cyan":
					return Color.cyan;
				case "magenta":
					return Color.magenta;
				case "gray":
					return Color.gray;
				case "grey":
					return Color.grey;
				default:
					throw new NotSupportedException(string.Format("[StringParserExtensions.ToColor] {0} not supported!", color));
			}
		}
	}
}