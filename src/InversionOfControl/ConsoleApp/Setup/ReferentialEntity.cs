namespace ConsoleApp.Setup
{

	public abstract class ReferentialEntity
	{
		private Guid referentialId;

		public ReferentialEntity()
		{
			referentialId = Guid.NewGuid();
		}

		public Guid ReferentialId()
		{
			return referentialId;
		}
	}
}
