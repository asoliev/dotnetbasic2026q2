using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Controllers;

public class SessionController(
    IBrainstormSessionRepository sessionRepository,
    ILogger<SessionController> logger) : Controller
{
    public async Task<IActionResult> Index(int? id)
    {
        if (!id.HasValue)
        {
            logger.LogDebug("Session index request did not include a session id.");
            return RedirectToAction(actionName: nameof(Index),
                controllerName: "Home");
        }

        logger.LogDebug("Loading session with id {SessionId}.", id.Value);

        BrainstormSession session = await sessionRepository.GetByIdAsync(id.Value);
        if (session == null)
        {
            logger.LogError("Session with id {SessionId} was not found.", id.Value);
            return Content("Session not found.");
        }

        logger.LogInformation("Loaded session with id {SessionId}.", session.Id);

        var viewModel = new StormSessionViewModel()
        {
            DateCreated = session.DateCreated,
            Name = session.Name,
            Id = session.Id
        };

        logger.LogDebug("Prepared session view model for session {SessionId}.", session.Id);

        return View(viewModel);
    }
}
