using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper 
    {//jwt hariç başka token uygulaması gelebilir.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); 
    }
}
