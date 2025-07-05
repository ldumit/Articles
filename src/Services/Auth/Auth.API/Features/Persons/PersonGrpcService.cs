﻿using Auth.API.Mappings;
using Auth.Grpc;
using Auth.Persistence.Repositories;
using ProtoBuf.Grpc;

namespace Auth.API.Features.Persons;


public class PersonGrpcService(PersonRepository _personRepository, GrpcTypeAdapterConfig _typeAdapterConfig) : IPersonService
{
		public async ValueTask<CreatePersonResponse> CreatePersonAsync(CreatePersonRequest request, CallContext context = default)
		{
				//todo - validate CreatePersonRequest, create a HTTP endpoint and use the same handler and validator
				var person = Person.Create(request);

				await _personRepository.AddAsync(person, context.CancellationToken);
				await _personRepository.SaveChangesAsync(context.CancellationToken);
				return new CreatePersonResponse
				{
						PersonInfo = person.Adapt<PersonInfo>(_typeAdapterConfig)
				};
		}
		public async ValueTask<GetPersonResponse> GetPersonByEmailAsync(GetPersonByEmailRequest request, CallContext context = default)
				=> await GetPersonResponseAsync(() => _personRepository.GetByEmailAsync(request.Email));

		public async ValueTask<GetPersonResponse> GetPersonByIdAsync(GetPersonRequest request, CallContext context = default)
				=> await GetPersonResponseAsync(() => _personRepository.GetByIdAsync(request.PersonId));


		public async ValueTask<GetPersonResponse> GetPersonByUserIdAsync(GetPersonByUserIdRequest request, CallContext context = default)
				=> await GetPersonResponseAsync(() => _personRepository.GetByUserIdAsync(request.UserId));

		private async ValueTask<GetPersonResponse> GetPersonResponseAsync(Func<Task<Person?>> fetch)
		{
				var person = Guard.NotFound(await fetch());
				var response = new GetPersonResponse
				{
						PersonInfo = person.Adapt<PersonInfo>(_typeAdapterConfig)
				};
				return response;
		}
}
