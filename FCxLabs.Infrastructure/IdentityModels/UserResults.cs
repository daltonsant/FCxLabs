using FCxLabs.Core.Contracts;

namespace FCxLabs.Infrastructure.IdentityModels;

public class UserResult : IUserResult
{
    public UserResult()
    {
        Errors = new List<string>();
    }
	
    public List<string> Errors { get; set; }
    public bool Succeeded 
    { 
        get
        {
            return Errors.Count <= 0;
        }
    }
}