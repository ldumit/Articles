using System.ComponentModel;

namespace Auth.Domain;

public enum Gender
{
		Male = 0,
		Female = 1,
		Neutral = 2,
		NotDeclared = 3
}

public enum UserRoleType : int
{
		[Description("Auth Admin")]
		AuthAdmin = 0,   // manage users
		[Description("Editorial Office")]
		EOF = 1,          //-->>Editorial Office
		[Description("Review Editor")]
		RE = 2,       //-->>Review Editor           
		[Description("Author")]
		AUT = 3,         //-->>Author
		[Description("Corresponding Author")] 
		CORAUT = 4,      //-->>Corresponding Author
		[Description("Submitting Author")] 
		SAUT = 5,        //-->>Submitting Author
		[Description("Co-Author")]
		COAUT = 6,       //-->>Co-author
		[Description("Production Office")]
		POF = 7,         //-->>Production Office
		[Description("Typesetter")]
		TSOF = 8,        //-->>Typesetting Office (Typesetter)
}
