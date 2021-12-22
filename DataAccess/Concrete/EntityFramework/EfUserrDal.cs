using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User userr)
        {
            using (var context = new ReCapContext()) //amaç iki tablonun joinlenmesi.
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == userr.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                //burada dto da kullanabilirdik. operationclaim yerine. entity'yi çirkinleştirip eklememe yapmamak için.
                return result.ToList();

            }
        }
    }
}
