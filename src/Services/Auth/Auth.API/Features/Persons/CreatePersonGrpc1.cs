//using Auth.Grpc;
//using Grpc.Core;

//namespace Auth.API.Features.Persons;

//public partial class PersonGrpcService1
//{
//		public override async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
//		{
//				var person = Person.Create(request);

//				await _personRepository.AddAsync(person, context.CancellationToken);
//				await _personRepository.SaveChangesAsync(context.CancellationToken);
//				return new CreatePersonResponse
//				{
//						PersonInfo = person.Adapt<PersonInfo>(_typeAdapterConfig)
//				};
//		}
//}
