using Contract;
using Contract.IHelper;
using Contract.Response;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RefTermService : IRefTermService
    {
        private readonly IRefSetRepo _refSetRepo;
        private readonly IRefSetTermRepo _refSetTermRepo;
        private readonly IRefTermRepo _refTermRepo;

        public RefTermService(
            IRefSetRepo refSetRepo,
            IRefSetTermRepo refSetTermRepo,
            IRefTermRepo refTermRepo)
        {
            _refSetRepo = refSetRepo;
            _refSetTermRepo = refSetTermRepo;
            _refTermRepo = refTermRepo;
        }

        public RefTermResponse CreateRefTerm(RefTerm refTermData, Guid refSetId)
        {

            var refSet = _refSetRepo.GetRefSet(refSetId);

            if (refSet == null)
                return new RefTermResponse(false, "Refset does not exists", null);

            var refTerm = _refTermRepo.GetRefTerm(refTermData.Key);

            if (refTerm != null)
            {
                var RefTerms = _refSetTermRepo.GetRefTermsByRefSetId(refSetId);
                foreach (var RefTerm in RefTerms)
                {
                    if (RefTerm.Key.ToLower() == refTermData.Key.ToLower())
                        return new RefTermResponse(false, "RefTerm already exists", null);
                }
                return new RefTermResponse(true, null, refTerm);
            }

            _refTermRepo.AddRefTerm(refTermData);
            _refSetRepo.Save();

            return new RefTermResponse(true, null, refTermData);
        }

        public void AddRefTermMapping(Guid refTermId, Guid refSetId)
        {
            var refSetMapping = new RefSetTerm
            {
                RefTermId = refTermId,
                RefSetId = refSetId
            };

            _refSetTermRepo.AddRefSetMapping(refSetMapping);
            _refSetRepo.Save();
        }

        public RefTermResponse GetRefTermByName(string key)
        {

            var refTerm = _refTermRepo.GetRefTerm(key);

            if (refTerm == null)
                return new RefTermResponse(false, "RefTerm does not exists", null);

            return new RefTermResponse(true, null, refTerm);
        }

        public RefTermResponse GetRefTermById(Guid refTermId)
        {

            var refTerm = _refTermRepo.GetRefTerm(refTermId);

            if (refTerm == null)
                return new RefTermResponse(false, "RefTerm does not exists", null);

            return new RefTermResponse(true, null, refTerm);
        }
    }
}
