namespace Review.Domain.Articles.Enums;

public enum ArticleActionType
{
		AssignEditor,
		CreateReviewer,
		InviteReviewer,
		AcceptInvitation,
		DeclineInvitation,
		UploadManuscript,
		UploadReviewReport,
		RevisionRequested,
		AcceptArticle,
		RejectArticle,
}
