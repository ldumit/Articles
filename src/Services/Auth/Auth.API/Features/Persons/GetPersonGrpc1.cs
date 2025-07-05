//using Auth.API.Mappings;
//using Auth.Grpc;
//using Auth.Persistence.Repositories;
//using Grpc.Core;

//namespace Auth.API.Features.Persons;

//public partial class PersonGrpcService1(PersonRepository _personRepository, GrpcTypeAdapterConfig _typeAdapterConfig) : PersonService.PersonServiceBase
//{
//		public override async Task<GetPersonResponse> GetPersonByEmail(GetPersonByEmailRequest request, ServerCallContext context)
//				=> await GetPersonResponseAsync(() => _personRepository.GetByEmailAsync(request.Email));

//		public override async Task<GetPersonResponse> GetPersonById(GetPersonRequest request, ServerCallContext context)
//				=> await GetPersonResponseAsync(()=> _personRepository.GetByIdAsync(request.PersonId));

//		public override async Task<GetPersonResponse> GetPersonByUserId(GetPersonByUserIdRequest request, ServerCallContext context)		
//				=> await GetPersonResponseAsync(() => _personRepository.GetByUserIdAsync(request.UserId));

//		private async Task<GetPersonResponse> GetPersonResponseAsync(Func<Task<Person?>> fetch)
//		{
//				var person = Guard.NotFound(await fetch());
//				var response = new GetPersonResponse
//				{
//						PersonInfo = person.Adapt<PersonInfo>(_typeAdapterConfig)
//				};
//				return response;
//		}

//		private async Task<GetPersonResponse> GetPersonResponseAsync(Func<CancellationToken, Task<Person?>> fetch, CancellationToken ct = default)
//		{
//				var person = Guard.NotFound(await fetch(ct));
//				return new GetPersonResponse
//				{
//						PersonInfo = person.Adapt<PersonInfo>(_typeAdapterConfig)
//				};
//		}
//}
