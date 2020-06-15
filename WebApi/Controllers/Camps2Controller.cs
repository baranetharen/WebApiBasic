using AutoMapper;
using Microsoft.Web.Http;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/v{version:apiVersion}/Camps")]
    public class Camps2Controller : ApiController
    {
        ICampRepository _iCampRepository;
        public IMapper Mapper { get; }

        public Camps2Controller(ICampRepository ICampRepository, IMapper mapper)
        {
            _iCampRepository = ICampRepository;
            Mapper = mapper;
        }

        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var result = await _iCampRepository.GetAllCampsAsync(includeTalks);
                var mapresult = Mapper.Map<CampModel[]>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{moniker}", Name = "GetCamp2")]
        public async Task<IHttpActionResult> Get(string moniker, bool includeTalks = true)
        {
            try
            {
                var result = await _iCampRepository.GetCampAsync(moniker, includeTalks);
                if (result == null) return NotFound();
                return Ok(new { success = true, camp = Mapper.Map<CampModel>(result) });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("SearchByDate/{date:datetime}"), HttpGet]
        public async Task<IHttpActionResult> SearchByEventDate(DateTime date, bool includeTalks)
        {
            try
            {
                var result = await _iCampRepository.GetAllCampsByEventDate(date, includeTalks);
                if (result == null) return NotFound();
                return Ok(Mapper.Map<CampModel[]>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> Post(CampModel campModel)
        {

            if (await _iCampRepository.GetCampAsync(campModel.Moniker) != null)
            {
                ModelState.AddModelError("Moniker", "Moniker in use");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var camp = Mapper.Map<Camp>(campModel);
                    _iCampRepository.Add(camp);
                    if (await _iCampRepository.SaveChangesAsync())
                    {
                        var campM = Mapper.Map<CampModel>(camp);
                        var location = Url.Link("GetCamp", new { moniker = campM.Moniker });
                        return Created(location, campM);
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{moniker}")]
        public async Task<IHttpActionResult> Put(string moniker, CampModel campModel)
        {
            try
            {
                var camp = await _iCampRepository.GetCampAsync(moniker);
                if (camp == null)
                {
                    return NotFound();
                }
                Mapper.Map(campModel, camp);
                if (await _iCampRepository.SaveChangesAsync())
                {
                    return Ok<CampModel>(Mapper.Map<CampModel>(camp));
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

        [HttpDelete]
        [Route("{moniker}")]
        public async Task<IHttpActionResult> Delete(string moniker)
        {
            try
            {
                var camp = await _iCampRepository.GetCampAsync(moniker);
                if (camp == null)
                {
                    return NotFound();
                }
                _iCampRepository.Delete(camp);
                if (await _iCampRepository.SaveChangesAsync())
                {
                    return Ok();
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
    }
}