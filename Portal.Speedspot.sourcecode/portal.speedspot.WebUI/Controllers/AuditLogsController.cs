using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.Models.Constants;
using portal.speedspot.WebUI.Controllers.Infrastructure;

namespace portal.speedspot.WebUI.Controllers
{
    public class AuditLogsController : BaseController
    {
        private readonly AuditLogsBiz _auditLogsBiz;

        public AuditLogsController(AuditLogsBiz auditLogsBiz)
        {
            _auditLogsBiz = auditLogsBiz;
        }

        [Authorize(Permissions.AuditLogs.View)]
        public async Task<IActionResult> Index(DateTime? dateSearch)
        {
            if (dateSearch == null)
            {
                dateSearch = DateTime.Now;
            }
            var logs = await _auditLogsBiz.GetByDateAsync(dateSearch.Value);

            ViewData["DateSearch"] = dateSearch.Value;
            return View(logs);
        }
    }
}
