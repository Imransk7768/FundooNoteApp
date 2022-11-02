using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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

        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteId == noteId);
                return result;
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = notesModel.Title;
                    result.Description = notesModel.Description;
                    result.Remainder = notesModel.Remainder;
                    result.Color = notesModel.Color;
                    result.Image = notesModel.Image;
                    result.Edited = DateTime.Now;
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
        public bool NotesDelete(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                if (result.Archive != true)
                {
                    result.Archive = true;
                }
                else if(result.Archive = true)
                {
                    result.Archive = false;
                }

                fundooContext.SaveChanges();
                return true;
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
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                if (result.Pin == false)
                {
                    result.Pin = true;
                }
                else if(result.Pin == true)
                {
                    result.Pin = false;
                }

                fundooContext.SaveChanges();
                return true;
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
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                if(result.Trash != true)
                {
                    result.Trash = true;
                }
                else if(result.Trash == true)
                {
                    result.Trash = false;
                }

                fundooContext.SaveChanges();
                return true;
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
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);


                if (result != null)
                {
                    Account account = new Account
                        (
                        this.iconfiguration["CloudinarySettings:CloudName"],
                        this.iconfiguration["CloudinarySettings:APIKey"],
                        this.iconfiguration["CloudinarySettings:APISecret"]
                        );

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    //Image as property given in Entity.
                    result.Image = imagePath;

                    fundooContext.SaveChanges();
                    return "Image Uploaded Successfilly";
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
        public string BackgroundColor(long noteId, string backgroundColor)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);
                result.Color = backgroundColor;
                fundooContext.NotesTable.Update(result);
                fundooContext.SaveChanges();
                return "BackgroundColor Change Successfull";

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
