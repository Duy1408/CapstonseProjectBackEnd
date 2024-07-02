﻿using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class OpenForSaleDetailDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public OpenForSaleDetailDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<OpenForSaleDetail> GetAllOpenForSaleDetail()
        {
            try
            {
                return _context.OpenForSaleDetails!.Include(c => c.OpeningForSale)
                                                   .Include(c => c.Property)
                                                   .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewOpenForSaleDetail(OpenForSaleDetail detail)
        {
            try
            {
                _context.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OpenForSaleDetail GetOpenForSaleDetailByID(Guid id)
        {
            try
            {
                var detail = _context.OpenForSaleDetails!.Include(c => c.OpeningForSale)
                                                         .Include(c => c.Property)
                                                         .SingleOrDefault(c => c.OpenForSaleDetailID == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOpenForSaleDetail(OpenForSaleDetail detail)
        {
            try
            {
                var a = _context.OpenForSaleDetails!.SingleOrDefault(c => c.OpenForSaleDetailID == detail.OpenForSaleDetailID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOpenForSaleDetailByID(Guid id)
        {
            var _detail = _context.OpenForSaleDetails!.SingleOrDefault(lo => lo.OpenForSaleDetailID == id);
            if (_detail != null)
            {
                _context.Remove(_detail);
                _context.SaveChanges();
            }
        }

    }
}
