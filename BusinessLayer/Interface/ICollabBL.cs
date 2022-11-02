using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(long notesId, string email);
        public IEnumerable<CollabEntity> RetrieveCollab(long notesId, long userId);
        public bool DeleteCollab(long collabId, long userId);
    }
}
