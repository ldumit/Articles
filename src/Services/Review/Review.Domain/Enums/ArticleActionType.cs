namespace Review.Domain.Enums;

//decide -  keep AssetActionType & ArticleActionType separetly?
public enum ArticleActionType
{
		Create,
		CreateAuthor,
		AssignEditor,
		InviteReviewer,
		Upload,
		Submit,
		Approve,
		Reject,
}
