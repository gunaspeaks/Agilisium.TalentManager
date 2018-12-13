using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface ISubPracticeService
    {
        bool Exists(string subPracticeName);

        bool Exists(int id);

        bool Exists(string subPracticeName, int id);

        List<SubPracticeDto> GetSubPractices();

        List<SubPracticeDto> GetSubPractices(int subPracticeID);

        SubPracticeDto GetSubPractice(int id);

        void CreateSubPractice(SubPracticeDto subPractice);

        void UpdateSubPractice(SubPracticeDto subPractice);

        void DeleteSubPractice(SubPracticeDto subPractice);
    }
}
