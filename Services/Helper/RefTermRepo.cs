using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.IHelper;
using Entities;
using Repository;

namespace Services.Helper
{
    public class RefTermRepo : IRefTermRepo
    {
        private readonly BookRepository _context;

        public RefTermRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddRefTerm(RefTerm refTermData)
        {
            if (refTermData == null)
                throw new ArgumentNullException(nameof(refTermData) + " was null in AddRefTerm from RefTermRepository.");

            _context.RefTerms.Add(refTermData);
        }

        public RefTerm GetRefTerm(Guid refTermId)
        {
            if (refTermId == null || refTermId == Guid.Empty)
                throw new ArgumentNullException(nameof(refTermId) + " was null in GetRefTerm from RefTermRepository.");

            return _context.RefTerms.SingleOrDefault(refTerm => refTerm.Id == refTermId);
        }

        public RefTerm GetRefTerm(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key) + " was null in GetRefTerm from RefTermRepository.");

            return _context.RefTerms.FirstOrDefault(refTerm => refTerm.Key.ToLower() == key.ToLower());
        }

        public void UpdateRefTerm(RefTerm refTermData)
        {
            if (refTermData == null)
                throw new ArgumentNullException(nameof(refTermData) + " was null in UpdateRefTerm from RefTermRepository.");

            _context.RefTerms.Update(refTermData);
        }

        public void DeleteRefTerm(RefTerm refTermData)
        {
            if (refTermData == null)
                throw new ArgumentNullException(nameof(refTermData) + " was null in DeleteRefTerm from RefTermRepository.");

            _context.RefTerms.Remove(refTermData);
        }
    }
}
