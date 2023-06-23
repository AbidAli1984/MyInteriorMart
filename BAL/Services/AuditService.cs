using BAL.Services.Contracts;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        public AuditService(IAuditRepository auditRepository)
        {
            this._auditRepository = auditRepository;
        }

        public Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid)
        {
            return _auditRepository.CheckIfUserBookmarkedListing(listingId, userGuid);
        }

        public Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            return _auditRepository.CheckIfUserLikedListing(listingId, userGuid);
        }

        public Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid)
        {
            return _auditRepository.CheckIfUserSubscribedToListing(listingId, userGuid);
        }
    }
}
