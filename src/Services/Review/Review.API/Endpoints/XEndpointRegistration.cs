namespace Review.API.Endpoints;

public static class EndpointRegistration
{
		public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
		{
				//todo - delete this class, it was replaced by Carter

				//GetArticleEndpoint.Map(app);
				//AssignEditorEndpoint.Map(app);
				//InviteReviewerEndpoint.Map(app);
				//UploadReviewReportEndpoint.Map(app);
				//UploadManuscriptFileEndpoint.Map(app);
				//AcceptArticleEndpoint.Map(app);
				//RejectArticleEndpoint.Map(app);
				//DownloadFileEndpoint.Map(app);

				return app;
		}
}
