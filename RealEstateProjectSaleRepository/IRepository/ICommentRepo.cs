using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface ICommentRepo
    {
        public bool ChangeStatus(Comment c);


        public List<Comment> GetComments();
        public void AddNew(Comment c);


        public Comment GetCommentById(Guid id);

        public void UpdateComment(Comment c);

        public IQueryable<Comment> SearchComment(string name);
        IQueryable<Comment> GetCommentByPropertyID(Guid id);
    }
}
