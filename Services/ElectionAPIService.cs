using ElectionApp.Models;
using ElectionApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElectionApp.Services
{
    public class ElectionAPIService: IApiServices
    {
        HttpClient _client;
        JsonSerializerOptions serializer;
        HttpResponseMessage _response;
        public ElectionAPIService() 
        {
            _client = new HttpClient(); 
            serializer = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }; 
        }
        public async Task<List<Parish>> GetParishes()
        {   
            List<Parish> parishes = new List<Parish>();
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/parish"));
            //Uri uri = new Uri(string.Format("https://localhost:7278/api/Parish"));
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode) 
            {
                string content = await _response.Content.ReadAsStringAsync();
                //parishes = JsonSerializer.Deserialize<List<Parish>>(content, serializer);
            }
            
            return parishes;
        }

        public async Task<List<Village>> GetVillages()
        {
            List<Village> villages = new List<Village>();
            Uri uri = new Uri(string.Format("https://localhost:7278/api/Village"));
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();                
            }

            return villages;
        }
        public async Task<List<Constituancy>> GetConstituancies()
        {
            List<Constituancy> constituancies = new List<Constituancy>();
            Uri uri = new Uri(string.Format("https://localhost:7278/api/Constituancy"));
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();                
            }

            return constituancies;
        }
        public async Task<List<Party>> GetParties()
        {
            List<Party> parties = new List<Party>();
            Uri uri = new Uri(string.Format("https://localhost:7278/api/Parish"));
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();
               
            }

            return parties;
        }
        public async Task<Person> GetPerson(int VotersID)
        {
            Person person = new Person();
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Person?PersonID=" + VotersID.ToString()));
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();
                person =JsonConvert.DeserializeObject<Person>(content);
            }

            return person;
        }
        public async Task<Boolean> VotersLogin(int VotersID, string Pin)
        {
            Boolean result = false;            
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Election?VotersID=" + VotersID.ToString() +"&Pin=" + Pin.ToString()));            
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Boolean>(content);        
            }

            return result;
        }
        public async Task<List<CandidateDisplay>> GetCandidateDisplays()
        {
            List<CandidateDisplay> candidates = new List<CandidateDisplay>();
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Candidate/"));            
            _response = await _client.GetAsync(uri);
            if (_response.IsSuccessStatusCode)
            {
                string content = await _response.Content.ReadAsStringAsync();
                candidates = JsonConvert.DeserializeObject<List<CandidateDisplay>>(content);                   
            }
            return candidates;
        }
        public async Task<bool> Postvote(Election election)
        {
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Election"));
            try
            {
                string json = JsonConvert.SerializeObject(election);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);
                if (_response.IsSuccessStatusCode) 
                {
                    return true;
                }
                else 
                { 
                    return false;
                }
            } 
            catch(Exception ex) 
            { 
              ex.Message.ToString();
                return false;
            }           
        }
        public async Task<Address> GetCurrentAddress(int AddressID)
        {
            Address address = new Address();
            try
            {
                Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Person?PersonID=" + AddressID.ToString()));
                _response = await _client.GetAsync(uri);
                if (_response.IsSuccessStatusCode)
                {
                    string content = await _response.Content.ReadAsStringAsync();
                    address = JsonConvert.DeserializeObject<Address>(content);
                }

                return address;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                address.AddressId = 0;
                return address;
            }
        }
        public async Task<bool> LockOutVoter(int personID)
        {
            Boolean result = false;
            Uri uri = new Uri(string.Format("http://mnideveloper-001-site1.btempurl.com/api/Election?personID=" + personID.ToString()+ "&vFlag=True"));
            try 
            {
                _response = await _client.GetAsync(uri);
                if (_response.IsSuccessStatusCode)
                {
                    string content = await _response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Boolean>(content);
                }

                return result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
       
    } 
}
