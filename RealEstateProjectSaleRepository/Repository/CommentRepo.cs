using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class CommentRepo : ICommentRepo
    {
        private CommentDAO _cmt;
        public CommentRepo()
        {
            _cmt = new CommentDAO();
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
            return _cmt.GetCommentByID(id);
        }

        public IQueryable<Comment> GetCommentByPropertyID(Guid id)
        {
            return _cmt.GetCommentByPropertyID(id);
        }

        public List<Comment> GetComments()
        {
            return _cmt.GetAllComment();
        }

        public IQueryable<Comment> SearchComment(string name)
        {
            return _cmt.SearchCommentByContent(name);
        }

        public void UpdateComment(Comment c)
        {
            _cmt.UpdateComment(c);
        }


    }
}
