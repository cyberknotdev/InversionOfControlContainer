namespace ConsoleApp.Setup
{
	public class FirstLevelA : ReferentialEntity, IFirstLevelA
	{
		private ISecondLevelA secondLevelA;
		private ISecondLevelB secondLevelB;

		public FirstLevelA(ISecondLevelA secondLevelA, ISecondLevelB secondLevelB)
		{
			this.secondLevelA = secondLevelA;
			this.secondLevelB = secondLevelB;
		}

		public string ObjectGraphAsString()
		{
			return "FirstLevelA - \t\t" + ReferentialId()
				+ "\n\t\t" + secondLevelA.ObjectGraphAsString()
				+ "\n\t\t" + secondLevelB.ObjectGraphAsString();
		}
	}
}
