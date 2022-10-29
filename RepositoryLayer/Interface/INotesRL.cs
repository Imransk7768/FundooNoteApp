using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId);

    }
}
