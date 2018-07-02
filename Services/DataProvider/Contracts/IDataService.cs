namespace Services.DataProvider.Contracts
{
    using MeTube.Models;
    using System.Collections.Generic;

    public interface IDataService
    {
        void RegisterUser(User user);

        bool IsUserExist(string username);

        void CreateTube(Tube tube);

        User GetUserByName(string username);

        Tube GetTubeById(int id);

        void IncreaseVies(Tube tube);

        IEnumerable<Tube> GetAllTubes();
    }
}
