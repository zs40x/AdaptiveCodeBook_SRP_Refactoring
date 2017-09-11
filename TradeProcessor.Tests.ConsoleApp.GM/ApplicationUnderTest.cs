namespace TradeProcessor.Tests.ConsoleApp.GM
{
    internal class ApplicationUnderTest
    {
        private readonly string _applicationExe;

        public ApplicationUnderTest(string applicationExe)
        {
            _applicationExe = applicationExe;
        }

        public string ConsoleOutput { get; private set; }

        public void Run()
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = _applicationExe,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };

            process.Start();
            process.StandardInput.WriteLine(@" ");
            
            ConsoleOutput = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
        }
    }
}
