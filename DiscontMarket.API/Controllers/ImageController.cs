using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Image;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        [HttpPost]
        [Route("upload-image")]
        public async Task<IActionResult> UploadImage([FromBody] SaveImageDTO dataToSend)
        {
            if (string.IsNullOrEmpty(dataToSend.imageName) || string.IsNullOrEmpty(dataToSend.image))
            {
                return BadRequest(new { error = "Имя изображения или данные отсутствуют." });
            }

            try
            {
                var targetDir = Path.Combine(Directory.GetCurrentDirectory(), "items", "productimages");

                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                var targetFile = Path.Combine(targetDir, dataToSend.imageName);

                // Конвертируем Base64 обратно в байты и сохраняем
                var imageBytes = Convert.FromBase64String(dataToSend.image);
                await System.IO.File.WriteAllBytesAsync(targetFile, imageBytes);


                //var imagePath = $"items/productimages/{dataToSend.imageName}" ;
                //var response = ResponseFactory<string>.CreateSuccessResponse(imagePath);

                var imagePath = $"items/productimages/{dataToSend.imageName}";
                return Ok(new { imagePath });

                //  return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при сохранении изображения: {ex.Message}" });
            }
        }


        [HttpPost]
        [Route("load-image")]
        public async Task<IActionResult> LoadImage([FromBody] ImageDTO dataToSend)
        {
            // Проверка на корректность переданных данных
            if (dataToSend == null || string.IsNullOrEmpty(dataToSend.Image) || string.IsNullOrEmpty(dataToSend.Path))
            {
                return BadRequest(new { error = "Файл не был загружен или путь не указан" });
            }

            // Получение изображения из base64
            byte[] imageBytes = Convert.FromBase64String(dataToSend.Image);

            // Проверка формата изображения (только PNG)
            if (!IsPng(imageBytes))
            {
                return BadRequest(new { error = "Допускаются только файлы формата .png" });
            }

            // Проверка размеров изображения
            var (width, height) = GetImageDimensions(imageBytes);
            if (width != dataToSend.Width || height != dataToSend.Height)
            {
                return BadRequest(new { error = $"Размер изображения должен быть {dataToSend.Width}x{dataToSend.Height}" });
            }

            // Перемещение изображения в нужное место
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dataToSend.Path);
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                return Ok(new { success = true, message = "Файл успешно заменен" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Не удалось сохранить файл", details = ex.Message });
            }
        }

        // Метод для проверки, является ли изображение PNG
        private bool IsPng(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                var img = Image.FromStream(ms);
                return img.RawFormat.Equals(ImageFormat.Png);
            }
        }

        // Метод для получения размеров изображения
        private (int width, int height) GetImageDimensions(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                var img = Image.FromStream(ms);
                return (img.Width, img.Height);
            }
        }

    }
}
