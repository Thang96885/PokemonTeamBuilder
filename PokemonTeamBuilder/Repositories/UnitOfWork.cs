using PokeApiNet;
using Shared_Library.Data;

namespace PokemonTeamBuilder.Api.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private readonly PokemonTeamBuilderContext _context;
		private readonly PokeApiClient _pokeClient;

		public UnitOfWork(PokemonTeamBuilderContext context, PokeApiClient pokeClient)
		{
			_context = context;
			_pokeClient = pokeClient;
		}

		private ITeamRepository _teamRepository;
		private IPokemonSetupRepository _pokemonSetupRepository;
		private IPokemonMoveChooseRepository _pokemonMoveChooseRepository;
		private IPokTypeRepository _typeRepository;

		public ITeamRepository TeamRepository { get
			{
				if(_teamRepository == null)
				{
					this._teamRepository = new TeamRepository(_context);
				}
				return _teamRepository;
			} }

		public IPokemonSetupRepository PokemonSetupRepository
		{
			get
			{
				if(_pokemonSetupRepository == null)
				{
					_pokemonSetupRepository = new PokemonSetupRepository(_context);
				}
				return _pokemonSetupRepository;
			}
		}

		public IPokemonMoveChooseRepository PokemonMoveChooseRepository
		{
			get
			{
				if (_pokemonMoveChooseRepository == null)
				{
					_pokemonMoveChooseRepository = new PokemonMoveChooseRepository(_context, _pokeClient);
				}
				return _pokemonMoveChooseRepository;
			}
		}

		public IPokTypeRepository TypeRepository
		{
			get
			{
				if (_typeRepository == null)
				{
					_typeRepository = new PokTypeRepository(_context);
				}
				return _typeRepository;
			}
		}

		public int SaveChange()
		{
			try
			{
				return _context.SaveChanges();
				
			}
			catch(Exception e)
			{
				return 0;
			}
		}

		public async Task<int> SaveChangeAsync()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch(Exception e)
			{
				return 0;
			}
		}

		private bool disposed = false;

		public void Dispose(bool disposing)
		{
            if (disposing)
            {
                if(!disposed)
				{
					_context.Dispose();
					disposed = true;
				}	
            }
        }
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
