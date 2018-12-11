using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IPracticeService
    {
        bool Exists(string practiceName);

        bool Exists(int id);

        bool Exists(string practiceName, int id);

        IEnumerable<PracticeDto> GetPractices();

        PracticeDto GetPractice(int id);

        void CreatePractice(PracticeDto practice);

        void UpdatePractice(PracticeDto practice);

        void DeletePractice(int id);
    }
}
