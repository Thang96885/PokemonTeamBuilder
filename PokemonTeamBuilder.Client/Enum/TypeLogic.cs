using PokemonTeamBuilder.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTeamBuilder.Client.Enums
{
	public enum TypeEnum:int
	{
		normal,
		fighting,
		flying,
		poison,
		ground,
		rock,
		bug,
		ghost,
		steel,
		fire,
		water,
		grass,
		electric,
		psychic,
		ice,
		dragon,
		dark,
		fairy,
	}


	public class TypeLogic
	{

		private readonly string[] _typeColor = { "#d7dbdd", "#d35400", "#A990F1", "#A03FA1", "#E1C167", "#B89F38", "#A8B820", "#715799", "#B8B8D0", "#F17F2E", "#6890F0", "#78C850", "#F9D130", "#F95788", "#95D7D8", "#7036FC", "#6F5747", "#6F5747" };

		private readonly double[] _teamDefence;

		private readonly double[] _teamTypeCoverage;

		private readonly int[] _pokemonTypeCount;

		private readonly int[] _pokemonAttackMoveTypeCount;

		private readonly double[,] TypeChart = new double[,]
		{
			{ 1, 1, 1, 1, 1, 0.5, 1, 0, 0.5, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // normal 0
			{ 2, 1, 0.5, 0.5, 1, 2, 0.5, 0, 2, 1, 1, 1, 1, 0.5, 2, 1, 2, 0.5 },// fighting 1
			{ 1, 2, 1, 1, 1, 0.5, 2, 1, 0.5, 1, 1, 2, 0.5, 1, 1, 1, 1, 1 }, // flying 2
			{ 1, 1, 1, 0.5, 0.5, 0.5, 1, 0.5, 0, 1, 1, 2, 1, 1, 1, 1, 1, 2 }, // posion 3
			{ 1, 1, 0, 2, 1, 2, 0.5, 1, 2, 2, 1, 0.5, 2, 1, 1, 1, 1, 1 },// ground 4
			{ 1, 0.5, 2, 1, 0.5, 1, 2, 1, 0.5, 2, 1, 1, 1, 1, 2, 1, 1, 1 },// rock 5
			{ 1, 0.5, 0.5, 0.5, 1, 1, 1, 0.5, 0.5, 0.5, 1, 2, 1, 2, 1, 1, 2, 0.5 },// bug 6
			{ 0, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5, 1 }, // ghost 7 
			{ 1, 1, 1, 1, 1, 2, 1, 1, 0.5, 0.5, 0.5, 1, 0.5, 1, 2, 1, 1, 2 }, // steel 8
			{ 1, 1, 1, 1, 1, 0.5, 2, 1, 2, 0.5, 0.5, 2, 1, 1, 2, 0.5, 1, 1 }, // fire 9
			{ 1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 0.5, 0.5, 1, 1, 1, 0.5, 1, 1 }, // water 10
			{ 1, 1, 0.5, 0.5, 2, 2, 0.5, 1, 0.5, 0.5, 2, 0.5, 1, 1, 1, 0.5, 1, 1 }, // grass 11
			{ 1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 2, 0.5, 0.5, 1, 1, 0.5, 1, 1 },// electic 12
			{ 1, 2, 1, 2, 1, 1, 1, 1, 0.5, 1, 1, 1, 1, 0.5, 1, 1, 0, 1 }, // psychic 13
			{ 1, 1, 2, 1, 2, 1, 1, 1, 0.5, 0.5, 0.5, 2, 1, 1, 0.5, 2, 1, 1 }, // ice 14
			{ 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 1, 1, 1, 1, 1, 1, 2, 1, 0 }, // dragon 15
			{ 1, 0.5, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5, 0.5 }, // dark 16
			{ 1, 2, 1, 0.5, 1, 1, 1, 1, 0.5, 0.5, 1, 1, 1, 1, 1, 2, 2, 1 } // fairy 17
		};

		public double[] TeamDefence 
		{ get
			{
				return _teamDefence;
			} 
		}

		public double[] TeamTypeCoverage 
		{ get
			{
				return _teamTypeCoverage;
			} 
		}

		public double GetEffectiveness(TypeEnum attackType, TypeEnum defenceType)
		{
			return TypeChart[(int)attackType, (int)defenceType];
		}

		public string GetTypeColor(TypeEnum type)
		{
			return _typeColor[(int)type];
		}

		public TypeLogic()
		{
			_teamDefence = new double[18];
			_teamTypeCoverage = new double[18];
			_pokemonTypeCount = new int[18];
			_pokemonAttackMoveTypeCount = new int[18];

			HelperFunction.Populate(_teamDefence, 0);
			HelperFunction.Populate(_teamTypeCoverage, 0);
			HelperFunction.Populate(_pokemonTypeCount, 0);
			HelperFunction.Populate(_pokemonAttackMoveTypeCount, 0);
			AddedPokemonType += OnAddPokemonType;
			AddedMoveType += OnAddMoveType;
		}

		public event Action<List<TypeEnum>> AddedPokemonType;
		public event Action<TypeEnum, List<TypeEnum>> AddedMoveType;

		public void AddPokemonType(List<TypeEnum> typeList)
		{
			for(int typeIndex = 0; typeIndex < typeList.Count; typeIndex++)
			{
				_pokemonTypeCount[(int)typeList[typeIndex]]++;
			}
			AddedPokemonType?.Invoke(typeList);
		}

		public void AddMoveType(TypeEnum moveType, List<TypeEnum> pokTypesEnum)
		{
			_pokemonAttackMoveTypeCount[(int)moveType]++;
			OnAddMoveType(moveType, pokTypesEnum);
		}

		public void RemoveMoveType(TypeEnum moveType, List<TypeEnum> pokTypesEnum)
		{
			for (int typeIndex = 0; typeIndex < 18; typeIndex++)
			{
				double typeAttackValue = TypeChart[(int)moveType, typeIndex];
				if (typeAttackValue == 2)
				{
					if (pokTypesEnum.Contains(moveType))
					{
						_teamTypeCoverage[typeIndex] -= 2;
					}
					else
					{
						_teamTypeCoverage[typeIndex]--;
					}
				}
			}
		}

		public void RemovePokemonType(List<TypeEnum> pokType)
		{
			double[] pokemonDefenceList = new double[18];
			HelperFunction.Populate(pokemonDefenceList, 0);

			for (int pokTypeIndex = 0; pokTypeIndex < pokType.Count; pokTypeIndex++)
			{
				for (int typeIndex = 0; typeIndex < 18; typeIndex++)
				{
					if (pokemonDefenceList[typeIndex] == 1.5)
					{
						continue;
					}
					double pokTypeDefenceValue = TypeChart[typeIndex, (int)pokType[pokTypeIndex]];
					if (pokTypeDefenceValue == 0)
					{
						pokemonDefenceList[typeIndex] += 1.5;
					}
					else if (pokTypeDefenceValue == 0.5)
					{
						pokemonDefenceList[typeIndex] += 1;
					}
					else if (pokTypeDefenceValue == 2)
					{
						pokemonDefenceList[typeIndex] -= 1;
					}
				}
			}

			for (int typeDefenceIndex = 0; typeDefenceIndex < 18; typeDefenceIndex++)
			{
				double value = pokemonDefenceList[typeDefenceIndex];
				if (value == 1 || value == -1)
				{
					_teamDefence[typeDefenceIndex] -= value;
				}
				else if (value == 1.5 || value == 2)
				{
					_teamDefence[typeDefenceIndex] -= 1.5;
				}
				else if (value == -2)
				{
					_teamDefence[typeDefenceIndex] += 1.5;
				}
			}
		}

		private void OnAddMoveType(TypeEnum moveType, List<TypeEnum> pokTypes)
		{
			for(int typeIndex = 0; typeIndex < 18; typeIndex++)
			{
				double typeAttackValue = TypeChart[(int)moveType, typeIndex];
				if(typeAttackValue == 2)
				{
					if(pokTypes.Contains(moveType))
					{
						_teamTypeCoverage[typeIndex] += 2;
					}
					else
					{
						_teamTypeCoverage[typeIndex]++;
					}
				}
			}
		}

		private void OnAddPokemonType(List<TypeEnum> pokType)
		{
			double[] pokemonDefenceList = new double[18];
			HelperFunction.Populate(pokemonDefenceList, 0);
			
			for(int pokTypeIndex = 0; pokTypeIndex < pokType.Count; pokTypeIndex++)
			{
				for(int typeIndex = 0; typeIndex < 18; typeIndex++)
				{
					if (pokemonDefenceList[typeIndex] == 1.5)
					{
						continue;
					}
					double pokTypeDefenceValue = TypeChart[typeIndex, (int)pokType[pokTypeIndex]];
					if (pokTypeDefenceValue == 0)
					{
						pokemonDefenceList[typeIndex] += 1.5;
					}
					else if(pokTypeDefenceValue == 0.5)
					{
						pokemonDefenceList[typeIndex] += 1;
					}
					else if(pokTypeDefenceValue == 2)
					{
						pokemonDefenceList[typeIndex] -= 1;
					}
				}
			}

			for(int typeDefenceIndex = 0; typeDefenceIndex < 18; typeDefenceIndex++)
			{
				double value = pokemonDefenceList[typeDefenceIndex];
				if(value == 1 || value == -1)
				{
					_teamDefence[typeDefenceIndex] += value;
				}
				else if(value == 1.5 || value == 2)
				{
					_teamDefence[typeDefenceIndex] += 1.5;
				}
				else if(value == -2)
				{
					_teamDefence[typeDefenceIndex] -= 1.5;
				}
			}
		}
	}
}
