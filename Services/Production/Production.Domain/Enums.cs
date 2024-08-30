using System.ComponentModel;

namespace Production.Domain.Enums;

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
public enum ActionType
{
    AssignTypesetter = 0,
    Upload = 1,
    Approve = 2,
    Published = 3,
    RequestNew = 4,
    UnApprove = 5,
    SchedulePublication = 9,
    Download = 11,
    Accepted = 101
}

public enum AssetType
{
    Manuscript = 1,
    ReviewReport = 2,
    AuthorsProof = 3,
    PublishersProof = 4,
    HTML = 5,
    XML = 6,
    Figure = 7,
    SupplementaryFile = 8,
}

public enum AssetStatus
{
    Deleted = 0,
    Requested = 1,
    Uploaded = 2,
    Approved = 3,
    ScheduledForPublication = 4,
    Published = 5
}

public enum ArticleStage
{
    [Description("Article submitted by the author")]
    Submitted = 100,
    [Description("Article approved")] 
    InReview = 200,
    [Description("Article accepted")]
    Accepted = 201,
    [Description("Typesetter assigned")]
    InProduction = 300,
    [Description("Author's proof uploaded")]
    AuthorsProof = 301,
    [Description("Author's proof approved")]
    FinalProduction= 302,
    [Description("Publisher's proof uploaded")]
    PublisherProof = 303,
    [Description("Article scheduled for publication")]
    PublicationScheduled = 304,
    [Description("Article published")]
    Published = 305    
}

public enum FileStatus
{
    UPLOADED = 1,
    APPROVED = 2,
    PUBLISHED = 3,
    NEW_VERSION_REQUESTED = 4,
    UN_APPROVED = 5,
    SYSTEM_GENERATED = 6,
    PENDING_PROVISION = 7,
    SCHEDULED_FOR_PUBLICATION = 9,
    DELETED = 10,
    DEPOSITED = 11,
    FAILED = 12
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

