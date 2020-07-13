﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockHollywoodBetsDAL.Models;
using Microsoft.AspNetCore.Cors;
using MockHollywoodBetsDAL.DataManagers;
using MockHollywoodBetsDAL.DataManagers.Repository;
using MockHollywoodBetsDAL.DataManagers.Repository.Interfaces;

namespace CRUDMockHollywoodBets.Controllers
{
    [Route("api/CRUD/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SportController : ControllerBase
    {
        private readonly ISportTreeRepository _sportTreeRepository;

        private readonly ILogger<SportController> _logger;

        public SportController(ILogger<SportController> logger, ISportTreeRepository sporttreerepository)
        {
            _logger = logger;
            _sportTreeRepository = sporttreerepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SportTree sportTree)
        {
            try
            {
                if (sportTree != null)
                {

                    _logger.LogInformation("API Request hit: INSERT Sport : " + sportTree.Name);
                    var result = _sportTreeRepository.Add(sportTree);

                    if (result == 0)
                    {
                        return Ok("{\"status\": \"Success\"}");
                    }
                    else
                    {
                        _logger.LogInformation("API Request (INSERT Sport : " + sportTree.Name + " ) not committed");
                        return NotFound("Failed: INSERT could not commit");
                    }

                }
                else
                {
                    _logger.LogInformation("API Request hit (INSERT Sport) with null entry");
                    return BadRequest("Failed: null entry");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("API Request (INSERT Sport) FAILED: ", e);
                return BadRequest("Failed");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] SportTree sportTree)
        {
            try
            {
                if (sportTree != null)
                {

                    _logger.LogInformation("API Request hit: UPDATE Sport : " + sportTree.Name);
                    var result = _sportTreeRepository.Update(sportTree);

                    if (result == 0)
                    {
                        return Ok("{\"status\": \"Success\"}");
                    }
                    else
                    {
                        _logger.LogInformation("API Request (UPDATE Sport : " + sportTree.Name + " ) not committed");
                        return NotFound("Failed: UPDATE could not commit");
                    }

                }
                else
                {
                    _logger.LogInformation("API Request hit (UPDATE Sport) with null entry");
                    return BadRequest("Failed: null entry");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("API Request (UPDATE Sport) FAILED: ", e);
                return BadRequest("Failed");
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SportTree sportTree)
        {
            try
            {
                if (sportTree != null)
                {

                    _logger.LogInformation("API Request hit: DELETE Sport : " + sportTree.Name);
                    var result = _sportTreeRepository.Delete(sportTree);

                    if (result == 0)
                    {
                        return Ok("{\"status\": \"Success\"}");
                    }
                    else
                    {
                        _logger.LogInformation("API Request (DELETE Sport : " + sportTree.Name + " ) not committed");
                        return NotFound("Failed: DELETE could not commit");
                    }

                }
                else
                {
                    _logger.LogInformation("API Request hit (DELETE Sport) with null entry");
                    return BadRequest("Failed: null entry");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("API Request (DELETE Sport) FAILED: ", e);
                return BadRequest("Failed");
            }
        }

        [HttpGet]
        public IActionResult Get(long? sportid)
        {
            try
            {
                if (sportid.HasValue)
                {

                    _logger.LogInformation("API Request hit: GET all Sports by Id: " + sportid.Value);
                    var result = _sportTreeRepository.Get(sportid.Value);
                    if (result.ToList().Any())
                    {
                        return Ok(result);
                    }
                    else
                    {
                        _logger.LogInformation("API Request (GET all Sports by SportId: " + sportid.Value + " ) no entries found");
                        return NotFound("Sport was not found with Id: " + sportid.Value);
                    }

                }
                else
                {
                    _logger.LogInformation("API Request hit: GET all Sports by no criteria");
                    var result = _sportTreeRepository.GetAll();
                    return Ok(result);
                }
            }

            catch (Exception e)
            {
                _logger.LogError("API Request (GET all Sports by Id) FAILED: ", e);
                return BadRequest();
            }

        }
    }
}