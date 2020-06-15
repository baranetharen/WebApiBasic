using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Camps/{moniker}/talks")]
    public class TalksController : ApiController
    {
        ICampRepository _iCampRepository;
        public IMapper Mapper { get; }

        public TalksController(ICampRepository ICampRepository, IMapper mapper)
        {
            _iCampRepository = ICampRepository;
            Mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var talks = await _iCampRepository.GetTalksByMonikerAsync(moniker, includeSpeakers);
                if (talks == null) return NotFound();
                return Ok(Mapper.Map<IEnumerable<TalkModel>>(talks));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("{id:int}", Name = "GetTalk")]
        public async Task<IHttpActionResult> Get(string moniker, int id, bool includeSpeakers = false)
        {
            try
            {
                var talks = await _iCampRepository.GetTalkByMonikerAsync(moniker, id, includeSpeakers);
                if (talks == null) return NotFound();
                return Ok(Mapper.Map<TalkModel>(talks));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> Post(string moniker, TalkModel talkModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var camp = await _iCampRepository.GetCampAsync(moniker);
                    if (camp != null)
                    {
                        var talk = Mapper.Map<Talk>(talkModel);
                        talk.Camp = camp;
                        _iCampRepository.Add(talk);
                        if (talkModel.Speaker != null)
                        {
                            var speaker = await _iCampRepository.GetSpeakerAsync(talkModel.Speaker.SpeakerId);
                            if (speaker != null) talk.Speaker = speaker;
                        }
                        if (await _iCampRepository.SaveChangesAsync())
                        {
                            var talkM = Mapper.Map<TalkModel>(talk);
                            var location = Url.Link("GetTalk", new { moniker, id = talkM.TalkId });
                            return Created(location, talkM);
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("{talkid:int}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(string moniker, int talkid, TalkModel talkModel)
        {
            try
            {
                var camp = await _iCampRepository.GetTalkByMonikerAsync(moniker, talkid);
                if (camp == null)
                {
                    return NotFound();
                }
                Mapper.Map(talkModel, camp);
                if (await _iCampRepository.SaveChangesAsync())
                {
                    return Ok<TalkModel>(Mapper.Map<TalkModel>(camp));
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{talkid:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string moniker, int talkid)
        {
            var talk = await _iCampRepository.GetTalkByMonikerAsync(moniker, talkid);
            if (talk == null) return NotFound();
            _iCampRepository.Delete(talk);
            if(await _iCampRepository.SaveChangesAsync())
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}