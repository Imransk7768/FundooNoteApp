using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);

        public NotesEntity NotesUpdate(NotesModel notesModel, long userId, long noteId);
        public bool NotesDelete(long userId, long noteId);
        public bool ArchiveNote(long noteId);
        public bool PinNote(long noteId);
        public bool TrashNote(long noteId);
        public string ImageUpload(IFormFile image, long noteId, long userId);
        public string BackgroundColor(long noteId, string backgroundColor);


    }
}
