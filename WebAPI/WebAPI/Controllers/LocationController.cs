using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LocationController : ApiController
    {
        //GET
        /// <summary>
        /// GET: Location details by default Order by Address asc
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllLocations()
        {
            try
            {
                List<Location> _locationDetails = null;
                using (var _dbContext = new LocationDbContext())
                {
                    _locationDetails = _dbContext.Locations.OrderBy(x => x.Address).ToList();
                }
                if (_locationDetails.Count() == 0)
                    return BadRequest("No Records Found");

                return Ok(_locationDetails);
            }
            catch
            {
                return InternalServerError();
            }
        }
        /// <summary>
        /// GET: Get Location details by Keyword Order by Address asc
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IHttpActionResult GetByKey(string _locationData)
        {
            try
            {
                List<Location> _locationDetails = null;
                if (_locationData.Length <= 2)
                    return BadRequest("Value must be 3 letters");
                using (var _dbContext = new LocationDbContext())
                {
                    _locationDetails = _dbContext.Locations
                                   .Where(x => x.Address.Contains(_locationData) || x.City.Contains(_locationData) || x.State.Contains(_locationData))
                                   .OrderBy(x => x.Address)
                                   .ToList();
                }
                return Ok(_locationDetails);
            }
            catch
            {
                if (_locationData == null)
                    return BadRequest("Value can not be null");
                return InternalServerError();
            }
        }
        
        public IHttpActionResult GetByKeywordAsc(string _locationData, string _orderBy)
        {
            try
            {
                List<Location> _locationDetails = null;
                if (_locationData.Length <= 2)
                    return BadRequest("Value must be 3 letters");
                using (var _dbContext = new LocationDbContext())
                {
                    if (_orderBy == "desc")
                    {
                        _locationDetails = _dbContext.Locations
                                   .Where(x => x.Address.Contains(_locationData) || x.City.Contains(_locationData) || x.State.Contains(_locationData))
                                   .OrderByDescending(x => x.Address)
                                   .ToList();

                    }
                    else if (_orderBy == "asc")
                    {
                        _locationDetails = _dbContext.Locations
                                  .Where(x => x.Address.Contains(_locationData) || x.City.Contains(_locationData) || x.State.Contains(_locationData))
                                  .OrderBy(x => x.Address)
                                  .ToList();
                    }
                }
                return Ok(_locationDetails);
            }
            catch
            {
                if (_locationData == null || _orderBy == null)
                    return BadRequest("Keyword/Value & Sort field can not be null");
                return InternalServerError();
            }
        }
        
        //POST
        /// <summary>
        /// GET: SaveLocation details
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult SaveLocation(Location _locationData)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data, Please recheck!");
                using (var _dbContext = new LocationDbContext())
                {
                    _dbContext.Locations.Add(new Location()
                    {
                        Address = _locationData.Address,
                        City = _locationData.City,
                        State = _locationData.State,
                        Zip = _locationData.Zip
                    });
                    _dbContext.SaveChanges();
                }
                return Ok("Data saved successfully.!");
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
