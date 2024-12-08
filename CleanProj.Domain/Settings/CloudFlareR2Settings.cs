namespace CleanProj.Domain.Settings;

public record CloudFlareR2Settings
{
    public string ServiceUrl { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string PromptPicsBucketName { get; set; }
    public string UserPicsBucketName { get; set; }
}