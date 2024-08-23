using System.ComponentModel;

namespace Auth.Domain;

public enum Gender
{
		NotDeclared = 0,
		Male = 1,
		Female = 2,
		Neutral = 3
}

//
public enum UserRoleType : int
{
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
		POF = 7,         //-->>Production Office Admin
		[Description("Typesetter")]
		TSOF = 8,        //-->>Typesetting Office (Typesetter)
		[Description("User Admin")]
		ADMIN = 100   // manage users
}
