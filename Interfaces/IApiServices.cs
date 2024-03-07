using ElectionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionApp.Interfaces
{
    public interface IApiServices
    {
        Task<List<Parish>> GetParishes();
        Task<Boolean> VotersLogin(int VotersID, string Pin);
        Task<List<CandidateDisplay>> GetCandidateDisplays();
        Task<Person> GetPerson(int VotersID);
        Task<bool> Postvote(Election election);
        Task<bool> LockOutVoter(int personID);
    }
}
