using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public interface IRepository<T>
    {
        void Add(T camp);
        void Delete(T camp);
    }
    public interface ICampRepository : IRepository<Camp>, IRepository<Talk> ,IRepository<Speaker>
    {
        Task<bool> SaveChangesAsync();

        // Camps
       
        Task<Camp[]> GetAllCampsAsync(bool includeTalks = false);
        Task<Camp> GetCampAsync(string moniker, bool includeTalks = false);
        Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);

        // Talks
        
        Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false);
        Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false);

        // Speakers
       
        Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker);
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<Speaker[]> GetAllSpeakersAsync();
    }
}
