using DinkToPdf;
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

        public byte[] GeneratePdfFromTemplate(string htmlContent)
        {
            var converter = new BasicConverter(new PdfTools());

            var globalSettings = new GlobalSettings
            {
                ColorMode = DinkToPdf.ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = DinkToPdf.PaperKind.A4,
                Margins = new MarginSettings { Top = 25, Bottom = 25, Left = 30, Right = 20 }
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var pdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return converter.Convert(pdfDocument);
        }

        public void AddNew(DocumentTemplate p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(DocumentTemplate p)
        {
            return _repo.ChangeStatus(p);
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
