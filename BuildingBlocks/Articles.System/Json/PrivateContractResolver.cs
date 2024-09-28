using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Articles.System.Json;

public class PrivateContractResolver : DefaultContractResolver
{
		//protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		//{
		//		var property = base.CreateProperty(member, memberSerialization);

		//		// Allow private setters to be accessed
		//		if (property.Writable || member is PropertyInfo prop && prop.GetSetMethod(true) != null)
		//		{
		//				property.Writable = true;
		//		}

		//		// Allow private fields to be serialized/deserialized
		//		else if (member is FieldInfo field && field.IsPrivate)
		//		{
		//				property.Writable = true;
		//				property.Readable = true;
		//		}


		//		return property;
		//}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
				var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
												.Select(p => base.CreateProperty(p, memberSerialization))
										.Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
															 .Select(f => base.CreateProperty(f, memberSerialization)))
										.ToList();
				props.ForEach(p => { p.Writable = true; p.Readable = true; });
				return props;
		}

		//protected override JsonContract CreateContract(Type objectType)
  //  {
  //      // Create the contract using the base method
  //      var contract = base.CreateContract(objectType);

  //      // Loop through the fields in the type
  //      foreach (var field in objectType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
  //      {
  //          // Create a property for each field
  //          var property = new JsonProperty
  //          {
  //              PropertyName = field.Name,
  //              PropertyType = field.FieldType,
  //              Readable = true,
  //              Writable = true,
  //              ValueProvider = new DynamicValueProvider(field)
  //          };

  //          // Add the new property to the contract
  //          //contract.Properties.Add(property);
  //      }

  //      return contract;
  //  }

}
