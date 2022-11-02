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
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;

        public CollabRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public CollabEntity CreateCollab(long notesId, string email)
        {
            try
            {
                var resultNote = fundooContext.NotesTable.Where(x => x.NoteId == notesId).FirstOrDefault();
                var resultEmail = fundooContext.Usertable.Where(x => x.Email == email).FirstOrDefault();

                if (resultEmail != null && resultNote != null)
                {
                    CollabEntity collabEntity = new CollabEntity();

                    collabEntity.Email = resultEmail.Email;
                    collabEntity.NoteId = resultNote.NoteId;
                    collabEntity.UserId = resultEmail.UserId;

                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
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
        public IEnumerable<CollabEntity> RetrieveCollab(long notesId, long userId)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(x => x.NoteId == notesId);
                if (result != null)
                {
                    return result;
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
    }
}
