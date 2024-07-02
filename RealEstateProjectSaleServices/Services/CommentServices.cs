using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class CommentServices : ICommentServices
    {
        ICommentRepo _cmt;
        public CommentServices(ICommentRepo cmt)
        {
            _cmt = cmt;
            
        }

        public void AddNew(Comment c)
        {
            _cmt.AddNew(c);
        }

        public bool ChangeStatus(Comment c)
        {
            return _cmt.ChangeStatus(c);
        }

        public Comment GetCommentById(Guid id)
        {
            return _cmt.GetCommentById(id);
        }

        public List<Comment> GetComments()
        {
            return _cmt.GetComments();
        }

        public IQueryable<Comment> SearchComment(string name)
        {
            return _cmt.SearchComment(name);
        }

      

        public void UpdateComment(Comment c)
        {
            _cmt.UpdateComment(c);
        }
    }
}
