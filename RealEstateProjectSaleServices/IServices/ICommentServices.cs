using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface ICommentServices
    {
        bool ChangeStatus(Comment c);


        List<Comment> GetComments();
        void AddNew(Comment c);


        Comment GetCommentById(Guid id);

        void UpdateComment(Comment c);

        IQueryable<Comment> SearchComment(string name);
        IQueryable<Comment> GetCommentByPropertyID(Guid id);
    }
}
