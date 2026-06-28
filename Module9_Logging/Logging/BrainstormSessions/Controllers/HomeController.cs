using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Controllers;

public class HomeController(
    IBrainstormSessionRepository sessionRepository,
    ILogger<HomeController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        logger.LogDebug("Loading home page session list.");

        List<BrainstormSession> sessionList = await sessionRepository.ListAsync();
        logger.LogInformation("Loaded {SessionCount} brainstorm sessions for the home page.", sessionList.Count);

        IEnumerable<StormSessionViewModel> model = sessionList.Select(session => new StormSessionViewModel()
        {
            Id = session.Id,
            DateCreated = session.DateCreated,
            Name = session.Name,
            IdeaCount = session.Ideas.Count
        });

        return View(model);
    }

    public class NewSessionModel
    {
        [Required]
        public string SessionName { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Index(NewSessionModel model)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError("Invalid session submission received for the home page.");
            return BadRequest(ModelState);
        }

        logger.LogDebug("Creating a new brainstorm session named {SessionName}.", model.SessionName);

        await sessionRepository.AddAsync(new BrainstormSession()
        {
            DateCreated = DateTimeOffset.Now,
            Name = model.SessionName
        });

        logger.LogInformation("Created a new brainstorm session named {SessionName}.", model.SessionName);

        return RedirectToAction(actionName: nameof(Index));
    }
}
