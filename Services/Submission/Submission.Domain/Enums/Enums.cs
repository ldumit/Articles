using System.ComponentModel;

namespace Submission.Domain.Enums;

public enum AssetCategory
{
    Core = 1,
    Supplementary = 2,
    Others = 3
}

//decide -  keep AssetActionType & ArticleActionType separetly?
public enum ArticleActionType
{
		Create,
		CreateAuthor,
		AssignAuthor,
		Upload,
		Submit,
		Approve,
		Reject,
}

public enum AssetActionType
{
    Upload,
    Approve,
    Request,
    CancelRequest,
    Download
}

public enum AssetState
{
    None = 0,
    Requested = 1,
    Uploaded = 2,
    Approved = 3
}

//todo unify it with AssetStatus and keep only one
public enum FileStatus
{
		Deleted = 0,
		Requested = 1,
		Uploaded = 2,
		Approved = 3,
		ScheduledForPublication = 4,
		Published = 5
}

public enum ArticleType
{
    OriginalResearch = 1,
		//SystematicReview = 2,
		//BriefResearchReport = 3,
		Editorial = 4,
		HypothesisAndTheory = 5,
		//Methods = 6,
		Opinion = 7,
		Review = 8,
    Correction = 9,
		//Other = 100
}

public enum ContributionArea
{
    //mandatory
    OriginalDraft = 1,
		ReviewAndEditing = 2,

    //optional
		Conceptualization = 3,
		FormalAnalysis = 4,
		Investigation = 5,
		Methodology = 6,
		Visualization = 7,
}

