using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.ClientModels;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Api;

public class IdeasController(
    IBrainstormSessionRepository sessionRepository,
    ILogger<IdeasController> logger) : ControllerBase
{

    #region snippet_ForSessionAndCreate
    [HttpGet("forsession/{sessionId}")]
    public async Task<IActionResult> ForSession(int sessionId)
    {
        logger.LogDebug("Loading ideas for session {SessionId}.", sessionId);

        BrainstormSession session = await sessionRepository.GetByIdAsync(sessionId);
        if (session == null)
        {
            logger.LogError("Unable to load ideas because session {SessionId} was not found.", sessionId);
            return NotFound(sessionId);
        }

        logger.LogInformation("Loaded {IdeaCount} ideas for session {SessionId}.", session.Ideas.Count, sessionId);

        var result = session.Ideas.Select(idea => new IdeaDTO()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError("Invalid idea submission received for session {SessionId}.", model?.SessionId);
            return BadRequest(ModelState);
        }

        logger.LogDebug("Creating an idea for session {SessionId}.", model.SessionId);

        BrainstormSession session = await sessionRepository.GetByIdAsync(model.SessionId);
        if (session == null)
        {
            logger.LogError("Unable to create idea because session {SessionId} was not found.", model.SessionId);
            return NotFound(model.SessionId);
        }

        var idea = new Idea()
        {
            DateCreated = DateTimeOffset.Now,
            Description = model.Description,
            Name = model.Name
        };
        session.AddIdea(idea);

        await sessionRepository.UpdateAsync(session);

        logger.LogInformation("Created a new idea named {IdeaName} for session {SessionId}.", model.Name, session.Id);

        return Ok(session);
    }
    #endregion

    #region snippet_ForSessionActionResult
    [HttpGet("forsessionactionresult/{sessionId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<IdeaDTO>>> ForSessionActionResult(int sessionId)
    {
        logger.LogDebug("Loading ideas action result for session {SessionId}.", sessionId);

        BrainstormSession session = await sessionRepository.GetByIdAsync(sessionId);

        if (session == null)
        {
            logger.LogError("Unable to load ideas action result because session {SessionId} was not found.", sessionId);
            return NotFound(sessionId);
        }

        logger.LogInformation("Loaded ideas action result for session {SessionId} with {IdeaCount} ideas.", sessionId, session.Ideas.Count);

        var result = session.Ideas.Select(idea => new IdeaDTO()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        return result;
    }
    #endregion

    #region snippet_CreateActionResult
    [HttpPost("createactionresult")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<BrainstormSession>> CreateActionResult([FromBody]NewIdeaModel model)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError("Invalid idea action result submission received for session {SessionId}.", model?.SessionId);
            return BadRequest(ModelState);
        }

        logger.LogDebug("Creating idea action result for session {SessionId}.", model.SessionId);

        BrainstormSession session = await sessionRepository.GetByIdAsync(model.SessionId);

        if (session == null)
        {
            logger.LogError("Unable to create idea action result because session {SessionId} was not found.", model.SessionId);
            return NotFound(model.SessionId);
        }

        var idea = new Idea()
        {
            DateCreated = DateTimeOffset.Now,
            Description = model.Description,
            Name = model.Name
        };
        session.AddIdea(idea);

        await sessionRepository.UpdateAsync(session);

        logger.LogInformation("Created idea action result named {IdeaName} for session {SessionId}.", model.Name, session.Id);

        return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
    }
    #endregion
}
