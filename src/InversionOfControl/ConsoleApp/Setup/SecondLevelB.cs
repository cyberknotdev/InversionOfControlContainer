namespace ConsoleApp.Setup
{
	public class SecondLevelB : ReferentialEntity, ISecondLevelB
	{
		public string ObjectGraphAsString()
		{
			return "SecondLevelB - \t" + ReferentialId();
		}
	}
}
