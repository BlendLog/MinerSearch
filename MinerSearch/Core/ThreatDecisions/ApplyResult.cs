namespace MSearch.Core.ThreatDecisions
{
    public enum ApplyResult
    {
        Success,       // Действие выполнено успешно
        Failed,        // Не удалось выполнить (файл залочен, процесс не завершился)
        Skipped,       // Пропущено (флаги не установлены, ScanOnly mode)
        NotApplicable, // Не применимо (файл уже удалён, процесс уже завершён)
        Error          // Ошибка (исключение)
    }
}
