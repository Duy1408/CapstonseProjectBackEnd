using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IDocumentTemplateService
    {
        bool ChangeStatus(DocumentTemplate p);
        List<DocumentTemplate> GetDocuments();
        void AddNew(DocumentTemplate p);
        DocumentTemplate GetDocumentById(Guid id);
        void UpdateDocument(DocumentTemplate p);
        IQueryable<DocumentTemplate> SearchDocument(string name);
    }
}
