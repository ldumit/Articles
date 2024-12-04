using System.ComponentModel;

namespace Articles.Security;

public enum UserRoleType : int
{
		[Description("Editorial Office")]
		EOF = 1,
		[Description("Author")]
		AUT = 2,
		[Description("Corresponding Author")]
		CORAUT = 3,
		[Description("Co-Author")]
		COAUT = 4,
		[Description("Review Editor")]
		REVED = 5,
		[Description("Reviewer")]
		REV = 6,
		[Description("Production Office")]
		POF = 7,
		[Description("Typesetting Office (Typesetter)")]
		TSOF = 8,
		[Description("User Admin")]
		ADMIN = 100
}

//todo replace the following enum with the above enum
public enum UserRoleType2 : int
{
		[Description("Editorial Office")]
		EditOF = 1,
		[Description("Author")]
		Auth = 2,
		[Description("Corresponding Author")]
		CorAuth = 3,
		[Description("Co-Author")]
		CoAuth = 4,
		[Description("Review Editor")]
		RevEd = 5,
		[Description("Reviewer")]
		Rev = 6,
		[Description("Production Office")]
		ProdOF = 7,
		[Description("Typesetting Office (Typesetter)")]
		TsetOF = 8,
		[Description("User Admin")]
		Admin = 100
}

public static class Role
{
		public const string ADMIN = nameof(UserRoleType.ADMIN);

		public const string EOF = nameof(UserRoleType.EOF);
		public const string CORAUT = nameof(UserRoleType.CORAUT);

		public const string REVED = nameof(UserRoleType.REVED);
		public const string REV = nameof(UserRoleType.REV);

		public const string POF = nameof(UserRoleType.POF);
		public const string TSOF = nameof(UserRoleType.TSOF);

}