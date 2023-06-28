﻿using BOL.IDENTITY;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ISuspendedUserRepository
    {
        Task<SuspendedUser> GetSuspendedUserById(string UserGuid);
        public Task SuspendUser(string SuspendedTo, string SuspendedBy, DateTime SuspendedDate, string ReasonForSuspending);
        public Task UnSuspendUser(string UnsuspendedTo, string UnsuspendedBy, DateTime UnsuspendedDate, string ReasonForUnsuspending);
        public Task<IEnumerable<SuspendedUser>> GetSuspendedUsers();
        public Task<IEnumerable<SuspendedUser>> GetUnSuspendedUsers();
    }
}
