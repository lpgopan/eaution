﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EAuction.Domain;

namespace EAuction.BusinessApplication.Persistance
{
    public interface ISellerRepository
    {
        /*
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
        */
        Task<Seller> AddSellerInformation(Seller seller);
    }
}
