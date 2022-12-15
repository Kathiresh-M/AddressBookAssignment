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
    public class RefSetRepo : IRefSetRepo
    {
        private readonly BookRepository _context;

        public RefSetRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddRefSet(RefSet refSetData)
        {
            if (refSetData == null)
                throw new ArgumentNullException(nameof(refSetData) + " was null in AddRefSet from RefSetRepository.");

            _context.RefSets.Add(refSetData);
        }

        public RefSet GetRefSet(Guid refSetId)
        {
            if (refSetId == null || refSetId == Guid.Empty)
                throw new ArgumentNullException(nameof(refSetId) + " was null in GetRefSet from RefSetRepository.");

            return _context.RefSets.SingleOrDefault(refSet => refSet.Id == refSetId);
        }

        public RefSet GetRefSet(string set)
        {
            if (string.IsNullOrEmpty(set))
                throw new ArgumentNullException(nameof(set) + " was null in GetRefSet from RefSetRepository.");

            return _context.RefSets.SingleOrDefault(refSet => refSet.Set.ToLower() == set.ToLower());
        }

        public void UpdateRefSet(RefSet refSetData)
        {
            if (refSetData == null)
                throw new ArgumentNullException(nameof(refSetData) + " was null in UpdateRefSet from RefSetRepository.");

            _context.RefSets.Update(refSetData);
        }

        public void DeleteRefSet(RefSet refSetData)
        {
            if (refSetData == null)
                throw new ArgumentNullException(nameof(refSetData) + " was null in DeleteRefSet from RefSetRepository.");

            _context.RefSets.Remove(refSetData);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
