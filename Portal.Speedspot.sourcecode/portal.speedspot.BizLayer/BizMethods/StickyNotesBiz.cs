using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class StickyNotesBiz : BizBaseT<StickyNote>
    {
        private static IStickyNotesRepository _repository;
        public StickyNotesBiz(IStickyNotesRepository repository):base(repository)
        {
            _repository=repository;
        }


        public async Task<List<StickyNote>> GetAllNotesByUserId(string userId)
        {
            return (await _repository.GetAllAsync(x => x.CreatedById == userId && x.IsArchived == false)).ToList();
        }
    }
}
