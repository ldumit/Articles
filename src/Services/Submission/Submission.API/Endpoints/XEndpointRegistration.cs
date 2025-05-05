namespace Submission.API.Endpoints;

public static class EndpointRegistration
{
		public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
		{
				GetArticleEndpoint.Map(app);
				CreateArticleEndpoint.Map(app);
				AssignAuthorEndpoint.Map(app);
				CreateAndAssignAuthorEndpoint.Map(app);
				UploadManuscriptFileEndpoint.Map(app);
				UploadSupplimentaryMaterialFileEndpoint.Map(app);
				SubmitArticleEndpoint.Map(app);
				ApproveArticleEndpoint.Map(app);
				RejectArticleEndpoint.Map(app);
				DownloadFileEndpoint.Map(app);
				
				return app;
		}
}
