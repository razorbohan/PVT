using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L6_P2_4_TagHelper.DAL;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace L6_P2_4_TagHelper.Middleware
{
    public class ExportMiddleware
    {
        private readonly RequestDelegate _next;
        public ExportMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPartyRepository partyRepository, IParticipantRepository participantRepository)
        {
            var path = context.Request.Path;
            if (path.StartsWithSegments("/export/users"))
            {
                var id = GetIdFromPath(path);
                var participants = id != -1
                    ? participantRepository.GetAll().Where(x => x.PartyId == id)
                    : participantRepository.GetAll();

                var sb = new StringBuilder();
                sb.Append("Id, Name, IsAttend, Avatar, Reason, PartyId\n");
                foreach (var participant in participants)
                {
                    sb.Append(participant.Id);
                    sb.Append("," + participant.Name);
                    sb.Append("," + participant.IsAttend);
                    sb.Append("," + participant.Avatar);
                    sb.Append("," + participant.Reason);
                    sb.Append("," + participant.PartyId);

                    sb.Append("\n");
                }

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/csv";
                context.Response.Headers.Add("Content-Disposition", "Attachment; filename=\"participants.csv\"");
                context.Response.ContentLength = bytes.Length;
                await context.Response.WriteAsync(sb.ToString());
            }
            else await _next(context);
        }

        private int GetIdFromPath(PathString path)
        {
            var id = path.Value.Replace("/export/users/", "");
            return !string.IsNullOrEmpty(id) ? int.Parse(id) : -1;
        }
    }

    public static class ExportMiddlewareExtension
    {
        public static IApplicationBuilder UseExportMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExportMiddleware>();
        }
    }
}
