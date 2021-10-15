using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RideShareCase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ILogger<TripController> _logger;
        private readonly IBusinessService _businessService;

        public TripController(ILogger<TripController> logger, IBusinessService businessService)
        {
            _logger = logger;
            _businessService = businessService;
        }

        /// <summary>
        /// Creates a Trip Plan as Passive
        /// </summary>
        /// <param name="plan">TripPlan to be added to the system</param>
        /// <response code="200">TripPlan has been added successfully</response>
        /// <response code="455">TripPlan has failed to add because of data corruption in database on City table</response>
        /// <response code="500">Technical error on the system</response>
        [HttpPost]
        [Route("CreateTripPlan")]
        public ActionResult<int> CreateTripPlan(TripPlanRequest plan)
        {
            try
            {
                return _businessService.CreateTripPlan(plan);
            }
            catch (Exception e)
            {
                if (e.Data["statusCode"] != null) return StatusCode(Int32.Parse(e.Data["statusCode"].ToString()), e.Data["exception"]);
                else return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Changes the state of Plan to Active or Passive
        /// </summary>
        /// <param name="tripId">Id of the trip to update</param>
        /// <param name="state">New state of the chosen trip</param>
        /// <response code="200">Trip has been succesfully updated</response>
        /// <response code="454">Trip plan state is already same</response>
        /// <response code="500">Technical error on the system</response>
        [HttpPost]
        [Route("UpdateTripPlan")]
        public ActionResult UpdateTripPlan(int tripId, bool state)
        {
            try
            {
                _businessService.UpdateTripPlan(tripId, state);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                if (e.Data["statusCode"] != null) return StatusCode(Int32.Parse(e.Data["statusCode"].ToString()), e.Data["exception"]);
                else return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Joins a user into a trip plan
        /// </summary>
        /// <param name="tripId">ID of trip plan</param>
        /// <response code="200">User succesfully added to trip plan</response>
        /// <response code="453">Trip plan has no empty seats</response>
        /// <response code="500">Technical error on the system</response>
        [HttpPost]
        [Route("JoinTripPlan")]
        public ActionResult JoinTripPlan(int tripId)
        {
            try
            {
                _businessService.JoinTripPlan(tripId);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                if (e.Data["statusCode"] != null) return StatusCode(Int32.Parse(e.Data["statusCode"].ToString()), e.Data["exception"]);
                else return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Searchs active trip plans for from location to destination location
        /// </summary>
        /// <param name="fromLocation">Trip's from location</param>
        /// <param name="toDestination">trip's destination location</param>
        /// <response code="200">All matching trip plans has been returned</response>
        /// <response code="204">No matching trip plans has been found</response>
        /// <response code="500">Technical error on the system</response>
        [HttpGet]
        [Route("SearchTripPlans")]
        public ActionResult<IEnumerable<TripPlan>> SearchTripPlans(string fromLocation, string toDestination)
        {
            try
            {
                if (_businessService.SearchTripPlans(fromLocation, toDestination).ToList().Count == 0) return StatusCode(204);
                else return _businessService.SearchTripPlans(fromLocation, toDestination).ToList();
            }
            catch (Exception e)
            {
                if (e.Data["statusCode"] != null) return StatusCode(Int32.Parse(e.Data["statusCode"].ToString()), e.Data["exception"]);
                else return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Searchs active trip plans for from location to destination location with part 2 from case
        /// </summary>
        /// <param name="fromLocation">Trip's from location</param>
        /// <param name="toDestination">trip's destination location</param>
        /// <response code="200">All matching trip plans has been returned</response>
        /// <response code="204">No matching trip plans has been found</response>
        /// <response code="500">Technical error on the system</response>
        [HttpGet]
        [Route("SearchTripPlansV2")]
        public ActionResult<IEnumerable<TripPlan>> SearchTripPlansV2(string fromLocation, string toDestination)
        {
            try
            {
                if (_businessService.SearchTripPlansV2(fromLocation, toDestination).ToList().Count == 0) return StatusCode(204);
                else return _businessService.SearchTripPlansV2(fromLocation, toDestination).ToList();
            }
            catch (Exception e)
            {
                if (e.Data["statusCode"] != null) return StatusCode(Int32.Parse(e.Data["statusCode"].ToString()), e.Data["exception"]);
                else return StatusCode(500, e);
            }
        }
    }
}
