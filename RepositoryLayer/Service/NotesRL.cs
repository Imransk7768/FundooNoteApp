using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;
        public NotesRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        //public NotesEntity notesEntity = new NotesEntity();
        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);
                if (result != null)
                {
                    notesEntity.UserId = userId;
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Remainder = notesModel.Remainder;
                    notesEntity.Color = notesModel.Color;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Edited = notesModel.Edited;

                    fundooContext.NotesTable.Add(notesEntity);

                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<NotesEntity> RetrieveNotes(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
