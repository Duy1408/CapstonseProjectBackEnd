using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class DocumentTemplateDAO
    {

        private static DocumentTemplateDAO instance;

        public static DocumentTemplateDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DocumentTemplateDAO();
                }
                return instance;
            }


        }

        public List<DocumentTemplate> GetAllDocument()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.DocumentTemplates.ToList();
        }

        public bool AddNewDocument(DocumentTemplate p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.DocumentTemplates.SingleOrDefault(c => c.DocumentTemplateID == p.DocumentTemplateID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.DocumentTemplates.Add(p);
                _context.SaveChanges();
                return true;
            }
        }


        public bool UpdateDocument(DocumentTemplate p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.DocumentTemplates.SingleOrDefault(c => c.DocumentTemplateID == p.DocumentTemplateID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(p);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(DocumentTemplate p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.DocumentTemplates.FirstOrDefault(c => c.DocumentTemplateID.Equals(p.DocumentTemplateID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

        public DocumentTemplate GetDocumentByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.DocumentTemplates.SingleOrDefault(a => a.DocumentTemplateID == id);
        }
        public IQueryable<DocumentTemplate> SearchDocumentByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.DocumentTemplates.Where(a => a.DocumentName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

        public DocumentTemplate GetDocumentByDocumentName(string tempName)
        {
            try
            {
                var _context = new RealEstateProjectSaleSystemDBContext();
                var document = _context.DocumentTemplates!.SingleOrDefault(c => c.DocumentName == tempName);
                return document;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
