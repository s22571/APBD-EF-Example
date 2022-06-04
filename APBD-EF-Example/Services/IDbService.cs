namespace APBD_EF_Example.Services;

public interface IDbService
{
    Task<StatusResponse> GetActionWithFiretrucksAsync(int idAction);

    Task<StatusResponse> AddFiretruckToActionAsync(int idAction, int idFiretruck);
}