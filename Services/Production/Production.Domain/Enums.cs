using System.ComponentModel;

namespace Production.Domain.Enums;

public enum AssetCategory
{
    CORE = 1,
    SUPPLEMENTARY = 2,
    OTHERS = 3
}

public enum DiscussionType
{
    Default = 0,
    Public = 1,
    Private = 2
}
public enum FileActionType
{
    AssignTypesetter = 0,
    UPLOAD = 1,
    APPROVE = 2,
    PUBLISHED = 3,
    REQUEST_NEW = 4,
    UN_APPROVE = 5,
    SCHEDULED_FOR_PUBLICATION = 9,
    DOWNLOAD = 11,
    ACCEPTED = 101
}
public enum AssetType
{
    [Description("Manuscript")]
    MANUSCRIPT = 1,
    [Description("Figure")]
    FIGURE = 2,
    [Description("Table")]
    TABLE = 3,
    [Description("Supplementary file")]
    SUPPLEMENTARY_FILE = 4,
    [Description("XML")]
    XML = 6,
    [Description("Author's proof")]
    AUTHORS_PROOF = 7,
}
public enum AssetStatus
{
    REQUESTED = 1,
    UPLOADED = 2,
    APPROVED = 3,
    PUBLISHED = 4,
    PUBLICATION_SCHEDULED = 5,
    PENDING_PROVISION = 6,
    DELETED = 7
}

public enum ArticleStagesCode
{
    [Description("All")]
    ALL = 0,
    [Description("Initial Assessment")]
    INITIAL_ASSESSMENT = 1,
    [Description("In Production")]
    IN_PRODUCTION = 2,
    [Description("Author's proof")]
    AUTHORS_PROOF = 3,
    [Description("Final Production")]
    FINAL_PRODUCTION = 4,
    [Description("Publisher's proof")]
    PUBLISHERS_PROOF = 5,
    [Description("Scheduled for Publication")]
    SCHEDULED_FOR_PUBLICATION = 6,
    [Description("Published")]
    PUBLISHED = 7,
    [Description("Deposition Summary")]
    DEPOSITED = 8,
    [Description("Scheduled for Publication (Publishing)")]
    PUBLISHING = 9,
    [Description("Pre Accept")]
    PRE_ACCEPT_MANUSCRIPT = 101
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

public enum CommentTypeTopic
{
    ADDITIONAL = 1,
    REVIEW = 2,
    EOF_TO_POF = 3
}

