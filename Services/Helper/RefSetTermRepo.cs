using Contract.IHelper;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class RefSetTermRepo : IRefSetTermRepo
    {
        private readonly BookRepository _context;

        public RefSetTermRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddRefSetMapping(RefSetTerm refSetMappingData)
        {
            _context.RefSetTerm.Add(refSetMappingData);
        }

        public RefSetTerm GetRefSetMapping(Guid refSetMappingId)
        {
            if (refSetMappingId == null || refSetMappingId == Guid.Empty)
                throw new ArgumentNullException(nameof(refSetMappingId) + " was null in GetRefSetMapping from RefSetMappingRepository.");

            return _context.RefSetTerm.SingleOrDefault(refSetMapping => refSetMapping.Id == refSetMappingId);
        }

        public IEnumerable<RefTerm> GetRefTermsByRefSetId(Guid refSetId)
        {
            if (refSetId == null || refSetId == Guid.Empty)
                throw new ArgumentNullException(nameof(refSetId) + " was null in GetRefTermsByRefSetId from RefSetMappingRepository.");

            //var refTerms = _context.RefTerms.("Select RefTerm.Id,RefTerm.Key, RefTerm.Description From RefSet Join RefSetMapping On RefSet.Id = RefSetMapping.RefSetId Join RefTerm on RefTerm.Id = RefSetMapping.RefTermId").ToList<RefTerm>();
            var refTerms = from refSet in _context.RefSets
                           join refSetmapping in _context.RefSetTerm
                           on refSet.Id equals refSetmapping.RefSetId
                           join refTerm in _context.RefTerms
                           on refSetmapping.RefTermId equals refTerm.Id
                           select new
                           {
                               Id = refTerm.Id,
                               Key = refTerm.Key,
                               Description = refTerm.Description,
                               RefSetId = refSet.Id,
                           };

            var terms = new List<RefTerm>();

            foreach (var refTerm in refTerms)
            {
                if (refSetId == refTerm.RefSetId)
                    terms.Add(new RefTerm { Id = refTerm.Id, Key = refTerm.Key, Description = refTerm.Description });
            }

            return terms;
        }

        public RefTerm GetParticularRefTermWithRefSetId(Guid refTermId, Guid refSetId)
        {
            if (refSetId == null || refSetId == Guid.Empty)
                throw new ArgumentNullException(nameof(refSetId) + " was null in GetParticularRefTermWithRefSetId from RefSetMappingRepository.");

            if (refTermId == null || refTermId == Guid.Empty)
                throw new ArgumentNullException(nameof(refTermId) + " was null in GetParticularRefTermWithRefSetId from RefSetMappingRepository.");

            var refTerms = from refSet in _context.RefSets
                           join refSetmapping in _context.RefSetTerm
                           on refSet.Id equals refSetmapping.RefSetId
                           join refTerm in _context.RefTerms
                           on refSetmapping.RefTermId equals refTerm.Id
                           select new
                           {
                               Id = refTerm.Id,
                               Key = refTerm.Key,
                               Description = refTerm.Description,
                               RefSetId = refSet.Id,
                           };
            var refterm = refTerms.SingleOrDefault(refTerm => refTerm.Id == refTermId && refTerm.RefSetId == refSetId);

            return new RefTerm { Id = refterm.Id, Key = refterm.Key, Description = refterm.Description };
        }

        public IEnumerable<RefSetTerm> GetRefTermMappingId(Guid refSetId)
        {
            if (refSetId == null || refSetId == Guid.Empty)
                throw new ArgumentNullException(nameof(refSetId) + " was null in GetRefTermsByRefSetId from RefSetMappingRepository.");

            var refSetmappings = from refSet in _context.RefSets
                                 join refSetmapping in _context.RefSetTerm
                                 on refSet.Id equals refSetmapping.RefSetId
                                 join refTerm in _context.RefTerms
                                 on refSetmapping.RefTermId equals refTerm.Id
                                 select new RefSetTerm
                                 {
                                     Id = refSetmapping.Id,
                                     RefSetId = refSetmapping.RefSetId,
                                     RefTermId = refSetmapping.RefTermId
                                 };

            var mappings = new List<RefSetTerm>();

            foreach (var refSetMapping in refSetmappings)
            {
                if (refSetId == refSetMapping.RefSetId)
                    mappings.Add(refSetMapping);
            }

            return mappings;
        }
    }
}
