using CloudinaryDotNet.Actions;

namespace PudgeSMomom.Services.Cloudinary
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsycn(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);


    }
}
