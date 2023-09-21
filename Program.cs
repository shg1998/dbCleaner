using DBCleaner.Services;

DBCleanerService.Instance.Start(ConfigurationsReaderService.Instance.GetConfigurations());
