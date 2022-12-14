using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL inotesRL;
        public NotesBL(INotesRL inotesRL)
        {
            this.inotesRL = inotesRL;
        }
        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {

            try
            {
                return inotesRL.CreateNotes(notesModel, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                return inotesRL.RetrieveNotes(userId,noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public NotesEntity NotesUpdate(NotesModel notesModel, long userId, long noteId)
        {
            try
            {
                return inotesRL.NotesUpdate(notesModel, userId, noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool NotesDelete(long userId, long noteId)
        {
            try
            {
                return inotesRL.NotesDelete(userId, noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ArchiveNote(long noteId)
        {
            try
            {
                return inotesRL.ArchiveNote(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool PinNote(long noteId)
        {
            try
            {
                return inotesRL.PinNote(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool TrashNote(long noteId)
        {
            try
            {
                return inotesRL.TrashNote(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ImageUpload(IFormFile image, long noteId, long userId)
        {
            try
            {
                return inotesRL.ImageUpload(image, noteId, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string BackgroundColor(long noteId, string backgroundColor)
        {
            try
            {
                return inotesRL.BackgroundColor(noteId, backgroundColor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
