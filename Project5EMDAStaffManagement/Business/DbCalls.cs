using Project5EMDAStaffManagement.Data;
using Project5EMDAStaffManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5EMDAStaffManagement.Business
{
    public class DbCalls : IDbCalls
    {
        private readonly StaffDbContext _context;

        public DbCalls(StaffDbContext context)
        {
            _context = context;
        }

        public void IncrementReasonCount(int id)
        { 
            var ReasonUsed = _context.Reasons.Find(id);
            ReasonUsed.ReasonCount++;
            _context.Update(ReasonUsed);
            _context.SaveChangesAsync();
        }
    }
}
