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
    public class DocumentTemplateRepo : IDocumentTemplateRepo
    {
        private DocumentTemplateDAO _dao;
        public DocumentTemplateRepo()
        {
            _dao = new DocumentTemplateDAO();
        }
        public void AddNew(DocumentTemplate p)
        {
            _dao.AddNewDocument(p);
        }

        public bool ChangeStatus(DocumentTemplate p)
        {
          return  _dao.ChangeStatus(p);
        }

        public DocumentTemplate GetDocumentById(Guid id)
        {
            return _dao.GetDocumentByID(id);
        }

        public List<DocumentTemplate> GetDocuments()
        {
            return _dao.GetAllDocument();
        }

        public IQueryable<DocumentTemplate> SearchDocument(string name)
        {
            return _dao.SearchDocumentByName(name);
        }

        public void UpdateDocument(DocumentTemplate p)
        {
            _dao.UpdateDocument(p);
        }
    }
}
