using ConsoleApp.Setup;
using Container;

var rootClass = new RootClass(
						new FirstLevelA(
							new SecondLevelA(),
							new SecondLevelB()),
						new FirstLevelB(
							new SecondLevelA(),
							new SecondLevelB()));

var containerTransient = new IocContainer();
containerTransient.BindTransient<IRootClass, RootClass>();
containerTransient.BindTransient<IFirstLevelA, FirstLevelA>();
containerTransient.BindTransient<IFirstLevelB, FirstLevelB>();
containerTransient.BindTransient<ISecondLevelA, SecondLevelA>();
containerTransient.BindTransient<ISecondLevelB, SecondLevelB>();

var rootClassIoc = containerTransient.GetObject<IRootClass>();

var secondLevelA = new SecondLevelA();
var secondLevelB = new SecondLevelB();

var rootClassScoped = new RootClass(
						new FirstLevelA(
							secondLevelA,
							secondLevelB),
						new FirstLevelB(
							secondLevelA,
							secondLevelB));

var containerScoped = new IocContainer();
containerScoped.BindTransient<IRootClass, RootClass>();
containerScoped.BindTransient<IFirstLevelA, FirstLevelA>();
containerScoped.BindTransient<IFirstLevelB, FirstLevelB>();
containerScoped.BindTransient<ISecondLevelA, SecondLevelA>();
containerScoped.BindScoped<ISecondLevelB, SecondLevelB>();

var rootClassIocScoped = containerScoped.GetObject<IRootClass>();

var containerSingleton = new IocContainer();
containerSingleton.BindTransient<IRootClass, RootClass>();
containerSingleton.BindTransient<IFirstLevelA, FirstLevelA>();
containerSingleton.BindTransient<IFirstLevelB, FirstLevelB>();
containerSingleton.BindScoped<ISecondLevelA, SecondLevelA>();
containerSingleton.BindSingleton<ISecondLevelB, SecondLevelB>();

var rootClassWithSingletonFirstCall = containerSingleton.GetObject<IRootClass>();
var rootClassWithSingletonSecondCall = containerSingleton.GetObject<IRootClass>();

Console.WriteLine("Transient");
Console.WriteLine(rootClass.ObjectGraphAsString());
Console.WriteLine();
Console.WriteLine(rootClassIoc.ObjectGraphAsString());
Console.WriteLine();
Console.WriteLine("------------------------------------------------");
Console.WriteLine();
Console.WriteLine("Scoped");
Console.WriteLine(rootClassScoped.ObjectGraphAsString());
Console.WriteLine();
Console.WriteLine(rootClassIocScoped.ObjectGraphAsString());
Console.WriteLine();
Console.WriteLine("------------------------------------------------");
Console.WriteLine();
Console.WriteLine("Singleton");
Console.WriteLine(rootClassWithSingletonFirstCall.ObjectGraphAsString());
Console.WriteLine();
Console.WriteLine(rootClassWithSingletonSecondCall.ObjectGraphAsString());