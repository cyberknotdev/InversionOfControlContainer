namespace ConsoleApp.Setup
{
	public class RootClass : ReferentialEntity, IRootClass
	{
		private IFirstLevelA firstLevelA;
		private IFirstLevelB firstLevelB;

		public RootClass(IFirstLevelA firstLevelA, IFirstLevelB firstLevelB)
		{
			this.firstLevelA = firstLevelA;
			this.firstLevelB = firstLevelB;
		}

		public string ObjectGraphAsString()
		{
			return "RootClass - \t\t\t" + ReferentialId()
				+ "\n\t" + firstLevelA.ObjectGraphAsString()
				+ "\n\t" + firstLevelB.ObjectGraphAsString();
		}
	}
}
