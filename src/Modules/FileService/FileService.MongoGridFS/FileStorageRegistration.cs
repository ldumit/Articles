using Blocks.Core;
using FileStorage.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public static class FileStorageRegistration
{
		public static IServiceCollection AddMongoFileStorage(this IServiceCollection services, IConfiguration config)
		{
				services.AddAndValidateOptions<MongoGridFsFileStorageOptions>(config);
				var options = config.GetSectionByTypeName<MongoGridFsFileStorageOptions>();

				services.AddSingleton<IMongoClient>(sp =>
				{
						return new MongoClient(config.GetConnectionStringOrThrow(options.ConnectionStringName));
				});

				services.AddSingleton(sp =>
				{
						var client = sp.GetRequiredService<IMongoClient>();
						return client.GetDatabase(options.DatabaseName);
				});

				services.AddSingleton(sp =>
				{
						var db = sp.GetRequiredService<IMongoDatabase>();
						return new GridFSBucket(db, new GridFSBucketOptions
						{
								BucketName = options.BucketName,
								ChunkSizeBytes = options.ChunkSizeBytes,
								WriteConcern = WriteConcern.WMajority,
								ReadPreference = ReadPreference.Primary
						});
				});

				services.AddScoped<IFileService, FileService>();

				return services;
		}
}
