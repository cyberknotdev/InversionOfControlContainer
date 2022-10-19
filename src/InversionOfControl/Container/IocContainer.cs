namespace Container
{
	public class IocContainer
	{
		private readonly Dictionary<Type, TypeWithLifetimeSetting> mappings;

		private readonly Dictionary<Type, object> singletonInstances;

		public IocContainer()
		{
			mappings = new Dictionary<Type, TypeWithLifetimeSetting>();
			singletonInstances = new Dictionary<Type, object>();
		}

		public void BindTransient<From, To>()
			where From : class
			where To : class, From
		{
			Bind<From, To>(LifetimeSetting.Transient);
		}

		public void BindScoped<From, To>()
			where From : class
			where To : class, From
		{
			Bind<From, To>(LifetimeSetting.Scoped);
		}

		public void BindSingleton<From, To>()
			where From : class
			where To : class, From
		{
			Bind<From, To>(LifetimeSetting.Singleton);
		}

		private void Bind<From, To>(LifetimeSetting lifetimeSetting)
		{
			var typeWithLifetimeSetting = new TypeWithLifetimeSetting(typeof(To), lifetimeSetting);
			mappings.Add(typeof(From), typeWithLifetimeSetting);
		}

		public T GetObject<T>() where T : class
		{
			return (T)GetObject(typeof(T));
		}

		private object GetObject(Type fromType)
		{
			return GetObject(fromType, new Dictionary<Type, object>());
		}

		private object GetObject(Type fromType, Dictionary<Type, object> scopedInstances)
		{
			if (!mappings.ContainsKey(fromType))
			{
				throw new Exception("No mapping for for type - " + fromType.Name);
			}

			var typeWithLifetimeSetting = mappings[fromType];
			var toType = typeWithLifetimeSetting.Type();
			var constructor = toType.GetConstructors().Single();
			var constructorParameters = new object[constructor.GetParameters().Length];
			for (var i = 0; i < constructorParameters.Length; i++)
			{
				var constructorParameter = constructor.GetParameters()[i];
				var constructorParameterType = constructorParameter.ParameterType;
				if (scopedInstances.ContainsKey(constructorParameterType))
				{
					constructorParameters[i] = scopedInstances[constructorParameterType];
				}
				else if (singletonInstances.ContainsKey(constructorParameterType))
				{
					constructorParameters[i] = singletonInstances[constructorParameterType];
				}
				else
				{
					constructorParameters[i] = GetObject(constructorParameterType, scopedInstances);
				}
			}
			var instance = Activator.CreateInstance(toType, constructorParameters);
			if (typeWithLifetimeSetting.LifetimeSetting() == LifetimeSetting.Scoped)
			{
				scopedInstances.Add(fromType, instance);
			}
			if (typeWithLifetimeSetting.LifetimeSetting() == LifetimeSetting.Singleton)
			{
				singletonInstances.Add(fromType, instance);
			}
			return instance;
		}

		private enum LifetimeSetting
		{
			Transient,
			Scoped,
			Singleton
		}

		private class TypeWithLifetimeSetting
		{
			private Type type;
			private LifetimeSetting lifetimeSetting;

			public TypeWithLifetimeSetting(Type type, LifetimeSetting lifetimeSetting)
			{
				this.type = type;
				this.lifetimeSetting = lifetimeSetting;
			}

			public Type Type()
			{
				return type;
			}

			public LifetimeSetting LifetimeSetting()
			{
				return lifetimeSetting;
			}

		}
	}
}