using System.Net;
using APBD_EF_Example.Entities;
using APBD_EF_Example.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_EF_Example.Services;

public class DbService : IDbService
{
    private readonly FireBrigadeContext _context;

    public DbService(FireBrigadeContext context)
    {
        _context = context;
    }

    public async Task<StatusResponse> GetActionWithFiretrucksAsync(int idAction)
    {
        var action = await _context.Actions.FirstOrDefaultAsync(e => e.IdAction == idAction);

        if (action == null)
        {
            return new StatusResponse() {StatusCode = HttpStatusCode.BadRequest, Content = "Action not found"};
        }

        var firetrucks = await _context.FireTruckActions
            .Where(e => e.IdAction == idAction)
            .OrderByDescending(e => e.AssignmentDate)
            .Select(e => e.IdFiretruck).ToListAsync();

        ActionDTO actionDto = new ActionDTO()
        {
            IdAction = action.IdAction,
            StartTime = action.StartTime,
            EndTime = action.EndTime,
            NeedSpecialEquipment = action.NeedSpecialEquipment,
            Firetrucks = firetrucks
        };

        // if (firetrucks.Count != 0)
        // {
            // actionDto.Firetrucks = firetrucks;
        // }

        return new StatusResponse() {StatusCode = HttpStatusCode.OK, Content = actionDto};
    }

    public async Task<StatusResponse> AddFiretruckToActionAsync(int idAction, int idFiretruck)
    {
        var action = await _context.Actions.FirstOrDefaultAsync(e => e.IdAction == idAction);

        if (action == null)
        {
            return new StatusResponse() {StatusCode = HttpStatusCode.BadRequest, Content = "Action not found"};
        }

        var firetruckActions = await _context.FireTruckActions
            .Where(e => e.IdAction == idAction).ToListAsync();

        if (firetruckActions.Count >= 3)
        {
            return new StatusResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Action already have maximum number of firetrucks assigned"
            };
        }
        
        var firetruck = await _context.FireTrucks.FirstOrDefaultAsync(e => e.IdFiretruck == idFiretruck);

        if (firetruck == null)
        {
            return new StatusResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Firetruck not found"
            };
        }

        var fireTruckInAction = await _context.FireTruckActions
            .FirstOrDefaultAsync(e => e.IdAction == idAction && e.IdFiretruck == idFiretruck);
        
        if (fireTruckInAction != null)
        {
            return new StatusResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Given firetruck is already assigned to this action"
            };
        }

        if (action.NeedSpecialEquipment && !firetruck.SpecialEquipment)
        {
            return new StatusResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Firetruck with special equipment is needed in this action"
            };
        }

        if (action.EndTime != null && action.EndTime < DateTime.Now)
        {
            return new StatusResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Action already ended"
            };
        }

        await _context.FireTruckActions.AddAsync(new FireTruckAction()
        {
            IdAction = idAction,
            IdFiretruck = idFiretruck,
            AssignmentDate = DateTime.Now
        });

        await _context.SaveChangesAsync();

        return new StatusResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Content = "Firetruck added to action"
        };
    }
}