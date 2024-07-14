//namespace Articles.Abstractions;
using System.ComponentModel;

namespace Production.Domain.Enums;

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
public enum ArticleStage
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
