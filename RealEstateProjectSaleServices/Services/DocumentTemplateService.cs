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
    public class DocumentTemplateService : IDocumentTemplateService
    {
        private readonly IDocumentTemplateRepo _repo;
        public DocumentTemplateService(IDocumentTemplateRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(DocumentTemplate p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(DocumentTemplate p)
        {
          return  _repo.ChangeStatus(p);
        }

        public DocumentTemplate GetDocumentById(Guid id)
        {
            return _repo.GetDocumentById(id);
        }

        public List<DocumentTemplate> GetDocuments()
        {
            return _repo.GetDocuments();
        }

        public IQueryable<DocumentTemplate> SearchDocument(string name)
        {
            return _repo.SearchDocument(name);
        }

        public void UpdateDocument(DocumentTemplate p)
        {
            _repo.UpdateDocument(p);
        }
    }
}
