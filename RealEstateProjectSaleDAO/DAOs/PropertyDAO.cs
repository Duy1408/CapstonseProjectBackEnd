﻿using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PropertyDAO
    {
        private static PropertyDAO instance;

        public static PropertyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyDAO();
                }
                return instance;
            }


        }

        public List<Property> GetAllProperty()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Properties.ToList();
        }

        public bool AddNew(Property p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.SingleOrDefault(c => c.PropertyID == p.PropertyID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Properties.Add(p);
                _context.SaveChanges();
                return true;

            }
        }


        public bool UpdateProperty(Property p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.SingleOrDefault(c => c.PropertyID == p.PropertyID);

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

        public bool ChangeStatus(Property p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.FirstOrDefault(c => c.PropertyID.Equals(p.PropertyID));


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

        public Property GetPropertyByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Properties.SingleOrDefault(a => a.PropertyID == id);
        }
       
    }
}
