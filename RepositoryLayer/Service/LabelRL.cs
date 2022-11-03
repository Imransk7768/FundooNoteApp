using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;

        }

        public LabelEntity CreateLabel(long notesId, long userId, string labelName)
        {
            try
            {
                // var labelResult = fundooContext.LabelTable.Where(x => x.LabelName == labelName).FirstOrDefault();
                var notesResult = fundooContext.NotesTable.Where(x => x.NoteId == notesId).FirstOrDefault();
                if (notesResult != null)
                {
                    LabelEntity labelEntity = new LabelEntity();

                    labelEntity.LabelName = labelName;
                    labelEntity.NoteId = notesResult.NoteId;
                    labelEntity.UserId = userId;

                    fundooContext.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
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
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelId == labelId);
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
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.FirstOrDefault(x => x.LabelId == labelId);

                fundooContext.LabelTable.Remove(result);

                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public LabelEntity EditLabel(long noteId, string labelName)
        {
            try
            {
                var result = fundooContext.LabelTable.FirstOrDefault(e => e.NoteId == noteId);

                if (result != null)
                {
                    result.LabelName = labelName;

                    fundooContext.SaveChanges();
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
