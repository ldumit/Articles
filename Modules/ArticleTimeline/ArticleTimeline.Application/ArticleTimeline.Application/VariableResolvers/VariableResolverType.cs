using System.ComponentModel;

namespace ArticleTimeline.Application.VariableResolvers;

public enum VariableResolverType
{
		[Description("DateTime")]
		DateTime,
		[Description("RoleUser")]
		RoleUser,
		[Description("UploadedFile")]
		UploadedFile,
		[Description("ArticleStage")]
		ArticleStage,
		[Description("UserName")]
		UserName,
		[Description("Message")]
		Message,
		[Description("SubmittedUserName")]
		SubmittedUserName,
		[Description("SubmittedUserRole")]
		SubmittedUserRole
}
