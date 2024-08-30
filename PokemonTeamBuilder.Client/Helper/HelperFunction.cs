using Microsoft.EntityFrameworkCore.Query.Internal;
using PokemonTeamBuilder.Client.Enums;
using System.Security.Claims;
using System.Text.Json;

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

		public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
		}

		private static byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}
