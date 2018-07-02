namespace MeTube.App
{
    using System;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using Data;
    using WebServer;

    public class Laucher
    {
        public static void Main()
        {
            var server = new WebServer(8000, new ControllerRouter(), new ResourceRouter());
            var db = new MeTubeDbContext();
            MvcEngine.Run(server, db);
        }
    }
}
