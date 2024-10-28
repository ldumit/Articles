using System.ComponentModel;

namespace Submission.Domain.Enums;

public enum AssetCategory
{
    Core = 1,
    Supplementary = 2,
    Others = 3
}

public enum DiscussionType
{
    Default = 0,
    Public = 1,
    Private = 2
}

//decide -  keep AssetActionType & ArticleActionType separetly?
public enum ArticleActionType
{
		Create,
		Submit
}

public enum AssetActionType
{
    Upload,
    Approve,
    Request,
    CancelRequest,
    Download
}

public enum FileStatus2
{
		Requested = 1,
		Uploaded = 2,
		Approved = 3
}

public enum AssetState
{
    None = 0,
    Requested = 1,
    Uploaded = 2,
    Approved = 3
}

//public enum ArticleStage
//{
//    [Description("Article submitted by the author")]
//    Submitted = 100,
//    [Description("Article approved")] 
//    InReview = 200,
//    [Description("Article accepted")]
//    Accepted = 201,
//    [Description("Typesetter assigned")]
//    InProduction = 300,
//    [Description("Author's proof uploaded")]
//    AuthorsProof = 301,
//    [Description("Author's proof approved")]
//    FinalProduction= 302,
//    [Description("Publisher's proof uploaded")]
//    PublisherProof = 303,
//    [Description("Article scheduled for publication")]
//    PublicationScheduled = 304,
//    [Description("Article published")]
//    Published = 305    
//}

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

public enum CommentType
{
    ADDITIONAL = 1,
    REVIEW = 2,
    EOF_TO_POF = 3
}

public enum UserRole
{
    [Description("Editorial Office")]
    EOF = 1,          //-->>Editorial Office
    [Description("Review Editor")]
    RE = 2,       //-->>Review Editor           
    AUT = 3,         //-->>Author
    CORAUT = 4,      //-->>Corresponding Author
    SAUT = 5,        //-->>Submitting Author
    COAUT = 6,       //-->>Co-author
    POF = 7,         //-->>Production Office
    TSOF = 8,        //-->>Typesetting Office (Typesetter)
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

