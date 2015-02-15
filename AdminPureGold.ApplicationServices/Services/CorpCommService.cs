using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.CorpComm;
using AdminPureGold.Repositories.Interfaces.CorpComm;
using AdminPureGold.Repositories.Repositories.CorpComm;

namespace AdminPureGold.ApplicationServices.Services
{
    public class CorpCommService : ICorpCommService
    {
        private readonly IUnitOfWorkCorpComm _unitOfWorkCorpComm;

        public CorpCommService(IUnitOfWorkCorpComm unitOfWorkCorpComm)
        {
            _unitOfWorkCorpComm = unitOfWorkCorpComm;           
        }

        public IEnumerable<McMessage> GetMessagesForPerson(Int32 personNumber)
        {
            return _unitOfWorkCorpComm.McMessageRepository.GetMessagesForPerson(personNumber).ToList();
        }

        public void InsertUpdate_MessageCenter(Int32 recipient, Int32 sender, DateTime? dueDate, String subject, String link, String body, Int32 priority)
        {
            var existingMessage = GetMessagesForPerson(recipient)
                .Where(s => s.AppId == 10 && s.AppSubId == 1 )
                .SingleOrDefault();

            if (existingMessage != null)
            {
                existingMessage.AcknowledgeDate = null;
                existingMessage.DoNotDisplay = false;
                existingMessage.DueDate = dueDate;
                existingMessage.EntityStateForGraphsUpdates = State.Modified;
                _unitOfWorkCorpComm.McMessageRepository.Update(existingMessage);
            }
            else
            {
                var newMessage = new McMessage();
                newMessage.RecipientWpersno = recipient;
                newMessage.SenderWpersno = 0;
                newMessage.CreateDate = DateTime.Now;
                newMessage.DueDate = dueDate;
                newMessage.DoNotDisplay = false;
                newMessage.AppId = 10;
                newMessage.AppSubId = 1;
                newMessage.SubjectText = subject;
                newMessage.MessageBody = body;
                newMessage.Priority = priority;
                newMessage.EntityStateForGraphsUpdates = State.Added;
                _unitOfWorkCorpComm.McMessageRepository.Update(newMessage);

            }
            _unitOfWorkCorpComm.Save();
        }

    }
}
