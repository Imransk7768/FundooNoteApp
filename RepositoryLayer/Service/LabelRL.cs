﻿using RepositoryLayer.Context;
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

    }
}