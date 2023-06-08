namespace ios_tests
{
    using System;
    using Framework.Config;

    internal static class Config
    {
        private static readonly Lazy<string> _baseUrl = new Lazy<string>(
            () =>
            {
                switch (AppConfig.baseUrl)
                {
                    case "local":
                        return "http://localhost:4200/";
                    case "appveyor":
                        return "http://localhost/ios/";
                    case "report_server":
                        return "http://54.88.83.235/ios/";
                    case "design_server":
                        return "http://54.208.64.217/ios/";
                    default:
                        throw new InvalidOperationException($"Unrecognized baseUrl value: {AppConfig.baseUrl}");
                }
            });

        public static string BaseUrl => _baseUrl.Value;
    }
}
