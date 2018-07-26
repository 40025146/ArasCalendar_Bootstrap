namespace ArasMVC01.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static WORK_ORDERRepository GetWORK_ORDERRepository()
		{
			var repository = new WORK_ORDERRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static WORK_ORDERRepository GetWORK_ORDERRepository(IUnitOfWork unitOfWork)
		{
			var repository = new WORK_ORDERRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}