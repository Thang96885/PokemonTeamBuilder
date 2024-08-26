using Microsoft.EntityFrameworkCore.Query.Internal;
using PokemonTeamBuilder.Client.Enums;

namespace PokemonTeamBuilder.Client.Helper
{
	public static class HelperFunction
	{
		public static void Populate<T>(this T[] arr, T value)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = value;
			}
		}

		public static TypeEnum GetTypeEnumByName(string typeName)
		{
			var typeEnumArr = Enum.GetValues(typeof(TypeEnum));
			foreach (TypeEnum typeEnum in typeEnumArr)
			{
				if (typeEnum.ToString().ToLower() == typeName.ToLower())
				{
					return typeEnum;
				}
			}
			return TypeEnum.normal;
		}
	}
}
