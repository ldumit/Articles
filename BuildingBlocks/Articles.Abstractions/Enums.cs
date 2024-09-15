//namespace Articles.Abstractions;
using System.ComponentModel;

namespace Articles.Abstractions;

//public enum DiscussionType
//{
//    Default = 0,
//    Public = 1,
//    Private = 2
//}

//public enum ActionType
//{
//		AssignTypesetter = 0,
//		Upload = 1,
//		Approve = 2,
//		Published = 3,
//		RequestNew = 4,
//		UnApprove = 5,
//		SchedulePublication = 9,
//		Download = 11,
//		Accepted = 101
//}

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
		DraftProduction = 301,
		[Description("Author's proof approved")]
		FinalProduction = 302,
		[Description("Article scheduled for publication")]
		PublicationScheduled = 304,
		[Description("Article published")]
		Published = 305
}