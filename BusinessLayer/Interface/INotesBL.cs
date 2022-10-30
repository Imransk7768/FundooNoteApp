﻿using CommonLayer.Model;
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


    }
}