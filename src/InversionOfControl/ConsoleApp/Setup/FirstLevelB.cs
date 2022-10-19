namespace ConsoleApp.Setup
{
	public class FirstLevelB : ReferentialEntity, IFirstLevelB
	{
		private ISecondLevelA secondLevelA;
		private ISecondLevelB secondLevelB;

		public FirstLevelB(ISecondLevelA secondLevelA, ISecondLevelB secondLevelB)
		{
			this.secondLevelA = secondLevelA;
			this.secondLevelB = secondLevelB;
		}

		public string ObjectGraphAsString()
		{
			return "FirstLevelB - \t\t" + ReferentialId()
				+ "\n\t\t" + secondLevelA.ObjectGraphAsString()
				+ "\n\t\t" + secondLevelB.ObjectGraphAsString();
		}
	}
}
