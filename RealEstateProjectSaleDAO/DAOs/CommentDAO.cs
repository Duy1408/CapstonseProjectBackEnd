using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class CommentDAO
    {
        private static CommentDAO instance;

        public static CommentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentDAO();
                }
                return instance;
            }


        }

        public List<Comment> GetAllComment()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Comments.Include(c => c.Customer)
                                    .Include(c => c.Property)
                                    .ToList();
        }

        public bool AddNew(Comment co)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Comments.SingleOrDefault(c => c.CommentId == co.CommentId);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Comments.Add(co);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdateComment(Comment c)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Comments.SingleOrDefault(a => a.CommentId == c.CommentId);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(c);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Comment c)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Comments.FirstOrDefault(c => c.CommentId.Equals(c.CommentId));


            if (a == null)
            {
                return false;
            }
            else
            {
                a.Status = false;
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

        public Comment GetCommentByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Comments.Include(c => c.Customer)
                                    .Include(c => c.Property)
                                    .SingleOrDefault(a => a.CommentId == id);
        }

        public IQueryable<Comment> GetCommentByPropertyID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Comments!.Include(c => c.Customer)
                                    .Include(c => c.Property)
                                    .Where(c => c.Status == true && c.PropertyID == id);
            return a;
        }

        public IQueryable<Comment> SearchCommentByContent(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Comments.Include(c => c.Customer)
                                    .Include(c => c.Property)
                                    .Where(a => a.Content.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }


    }
}
