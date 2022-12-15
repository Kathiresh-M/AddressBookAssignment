using Contract;
using Contract.IHelper;
using Contract.Response;
using Entities;
using Entities.Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RefSetService : IRefSetService
    {
        private readonly IRefSetRepo _refSetRepository;
        private readonly IRefSetTermRepo _refSetMappingRepository;
        private readonly IRefTermRepo _refTermRepository;
        private readonly BookRepository _bookRepository;

        public RefSetService(
            IRefSetRepo refSetRepository,
            IRefSetTermRepo refSetMappingRepository,
            IRefTermRepo refTermRepository, BookRepository bookRepository)
        {
            _refSetRepository = refSetRepository;
            _refSetMappingRepository = refSetMappingRepository;
            _refTermRepository = refTermRepository;
            _bookRepository = bookRepository;
        }

        public RefSetResponse CreateRefSet(RefSet refSetData)
        {
            var refSet = _refSetRepository.GetRefSet(refSetData.Set);

            if (refSet != null)
                return new RefSetResponse(false, "Ref Set already exists", null);

            _refSetRepository.AddRefSet(refSetData);
            _refSetRepository.Save();

            return new RefSetResponse(true, null, refSetData);
        }

        public RefSetResponse GetRefSetByName(string set)
        {

            var refSet = _refSetRepository.GetRefSet(set);

            if (refSet == null)
                return new RefSetResponse(false, "Refset does not exists", null);

            return new RefSetResponse(true, null, refSet);
        }

        public RefSetResponse GetRefSetById(Guid setId)
        {

            var refSet = _refSetRepository.GetRefSet(setId);

            if (refSet == null)
                return new RefSetResponse(false, "Refset does not exists", null);

            return new RefSetResponse(true, null, refSet);
        }

        public RefSetTermResponse GetRefTermsByRefSetId(Guid setId)
        {

            var refSet = _refSetRepository.GetRefSet(setId);

            if (refSet == null)
                return new RefSetTermResponse(false, "Refset does not exists", null);

            var refTerms = _refSetMappingRepository.GetRefTermsByRefSetId(setId);

            int count = refTerms.Count();
            if (count == 0)
            {
                return new RefSetTermResponse(false, $"There are no RefTerms under ref set Id: {setId}", null);
            }

            return new RefSetTermResponse(true, null, refTerms);
        }

        public RefSetResponse DeleteRefSetById(Guid setId)
        {

            var refSet = _refSetRepository.GetRefSet(setId);

            if (refSet == null)
                return new RefSetResponse(false, "Refset does not exists", null);

            _refSetRepository.DeleteRefSet(refSet);
            _refSetRepository.Save();

            return new RefSetResponse(true, null, refSet);
        }

        public MetadataDto Metadata(string key)
        {
            var metadata = new MetadataDto();
            metadata.Id = _bookRepository.RefSets.Where(a => a.Set == key).Select(a => a.Id).SingleOrDefault();
            metadata.Key = key;
            return metadata;
        }
    }
}
