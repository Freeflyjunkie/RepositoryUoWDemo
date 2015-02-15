using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Repositories.Mrc;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.ApplicationServices.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;
        public SurveyService(IUnitOfWorkMrc unitOfWorkMrc)
        {
            _unitOfWorkMrc = unitOfWorkMrc;
        }
        public IEnumerable<SurveyQuestion> GetActiveQuestions()
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetQuestions(true).ToList();
        }
        public IEnumerable<SurveyQuestion> GetInActiveQuestions()
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetQuestions(false).ToList();
        }
        public IEnumerable<SurveyChoice> GetActiveChoices()
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetChoices(true).ToList();
        }
        public IEnumerable<SurveyChoice> GetInActiveChoices()
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetChoices(false).ToList();
        }
        public SurveyChoiceType GetChoiceTypeById(int choiceTypeId)
        {
            var sct = new SurveyChoiceType();
            return sct;
        }

        public IEnumerable<SurveyQuestion> GetQuestionByChoiceId(Int32 choiceId)
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetQuestionByChoiceId(choiceId);
        }
        public IEnumerable<SurveyChoice> GetChoiceByChoiceId(Int32 choiceId)
        {
            return _unitOfWorkMrc.SurveyQuestionRepository.GetChoiceByChoiceId(choiceId);
        }



        public IEnumerable<Survey> GetSurveyBySaleId(int saleId)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveysBySaleId(saleId).ToList();
        }
        public IEnumerable<Survey> GetSurveyBySurveyId(int surveyId)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveysBySurveyId(surveyId).ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswersBySurveyId(int surveyId)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveyAnswers(surveyId).ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswersBySaleId(int saleId)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveyAnswersBySaleId(saleId).ToList();
        }
        public IEnumerable<SurveyAnswerText> GetSurveyAnswersTextBySaleId(int saleId)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveyAnswersTextBySaleId(saleId).ToList();
        }

        public IEnumerable<SurveyReport> GetSurveyReportDetail(int choiceId, DateTime startDate, DateTime endDate)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<SurveyReport>();
            var result = mrcCoreSqlQueryRepository.GetSurveyReportDetail(choiceId, startDate, endDate).ToList();
            return result;
        }
        
        public IEnumerable<Survey> GetSurveyByDateRange(DateTime startDate, DateTime endDate)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveysByDateRange(startDate, endDate).ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _unitOfWorkMrc.SurveyRepository.GetSurveyAnswersByDateRange(startDate, endDate).ToList();
        }

        // TO DO : Change to use survey repository.  How do I pass in surveyAnswer
        public void SaveSurvey(string referenceNumber, int saleId, string inputBy)
        {
            var survey = new Survey
            {
                SaleId = saleId, 
                InputDate = DateTime.Now,
                InputBy = inputBy
            };

            _unitOfWorkMrc.SurveyRepository.Insert(survey);
            _unitOfWorkMrc.Save();
            var id = survey.SurveyId;

            #region Comments
            // var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<SurveyAnswer>();
            // mrcCoreSqlQueryRepository.SaveSurvey(referenceNumber, saleId, inputBy);
            #endregion            
        }
        public void SaveSurveyAnswer(int surveyId, int choiceId)
        {
           var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<SurveyAnswer>();
           mrcCoreSqlQueryRepository.SaveSurveyAnswer(surveyId, choiceId);

            #region Comments
            //var survey = _unitOfWorkMrc.SurveyRepository.GetSurveysBySurveyId(surveyId).FirstOrDefault();

            //if (survey != null)
            //{
            //    var exists = survey.SurveyAnswers.Where(a => a.ChoiceId == choiceId).ToList().FirstOrDefault();
            //    if (exists == null )
            //    {
            //        survey.SurveyAnswers = new List<SurveyAnswer> {
            //        new SurveyAnswer { 
            //            SurveyId = surveyId,
            //            ChoiceId=choiceId,
            //            EntityStateForGraphsUpdates = State.Added
            //            }
            //        };

            //        _unitOfWorkMrc.SurveyRepository.Update(survey);
            //        _unitOfWorkMrc.Save();
            //    }
            //}


            // SurveyAnswer surveyAnswer = new SurveyAnswer();
            // surveyAnswer.SurveyId = surveyId;
            // surveyAnswer.ChoiceId = choiceId;
            // surveyAnswer.EntityStateForGraphsUpdates = State.Modified;
            // _unitOfWorkMrc.SurveyRepository.S .Update(surveyAnswer);
            // _unitOfWorkMrc.Save();
            #endregion           
        }
        public void SaveSurveyAnswerText(int surveyId, int choiceId, string surveyAnswerText)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<SurveyAnswer>();
            mrcCoreSqlQueryRepository.SaveSurveyAnswerText(surveyId, choiceId, surveyAnswerText);
        }
        public void DeleteSurveyAnswer(int surveyId, int choiceId)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<SurveyAnswer>();
            mrcCoreSqlQueryRepository.DeleteSurveyAnswer(surveyId, choiceId);
        }        
    }
}
