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


public enum ArticleStage : int
{
		//Submission
		[Description("Author uploaded the Draft Manuscript")]
		DraftSubmission = 1,
		[Description("Author finished the Draft Manuscript")]
		Submitted = 2,
		[Description("Editorial team performs the initial check")]
		UnderInitialCheck = 3,

		//Review
		[Description("Article is under review")]
		UnderReview = 101,
		[Description("Reviewer feedback received")]
		ReviewerFeedback = 102,
		[Description("Revision requested")]
		RevisionRequested = 103,
		[Description("Article accepted")]
		Accepted = 104,
		[Description("Article rejected")]
		Rejected = 105,

		//Production
		[Description("Typesetter assigned")]
		InProduction = 300,
		[Description("Draft PDF uploaded")]
		DraftProduction = 301,
		[Description("Draft PDF approved")]
		FinalProduction = 302,
		[Description("Article scheduled for publication")]
		PublicationScheduled = 304,
		[Description("Article published")]
		Published = 305
}
