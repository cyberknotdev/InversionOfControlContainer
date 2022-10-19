namespace ConsoleApp.Setup
{
	public class SecondLevelA : ReferentialEntity, ISecondLevelA
	{
		public string ObjectGraphAsString()
		{
			return "SecondLevelA - \t" + ReferentialId();
		}
	}
}
