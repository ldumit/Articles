using System.ComponentModel;

namespace Articles.Abstractions.Enums;

public enum ArticleStage : int
{
		None = 0,

		//Submission
		[Description("The Author created the article")]
		Created = 101,
		//todo - intruduce DraftSubmission when the author is upload the Manuscript
		[Description("Author uploaded the Manuscript")]
		ManuscriptUploaded = 102,
		[Description("The Manuscript was submitted by the author")]
		Submitted = 103,
		[Description("The Manuscript didn't pass the initial checks")]
		InitialRejected = 104,
		[Description("The Manuscript passed the initial editorial checks")]
		InitialApproved = 105,

		//Review
		[Description("Article is under review")]
		InReview = 201,
		[Description("Reviewer feedback received")]
		ReviewerFeedback = 202,
		[Description("Revision requested")]
		RevisionRequested = 203,
		[Description("Article accepted")]
		Accepted = 204,
		[Description("Article rejected")]
		Rejected = 205,

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
