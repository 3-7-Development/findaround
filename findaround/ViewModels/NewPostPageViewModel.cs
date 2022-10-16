using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;

namespace findaround.ViewModels
{
	public partial class NewPostPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;

        ImageSource myImage;
        public ImageSource MyImage { get => myImage; set => SetProperty(ref myImage, value); }

		public NewPostPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "NewPostPage";
		}

        //[RelayCommand]
        //async Task UploadImages()
        //{
        //    IsBusy = true;

        //    //var imgs = await FilePicker.PickMultipleAsync(new PickOptions
        //    //{
        //    //    PickerTitle = "Pick image",
        //    //    FileTypes = FilePickerFileType.Images
        //    //});

        //    var imgs = await MediaPicker.Default.CapturePhotoAsync();

        //    if (imgs is null)
        //        return;

        //    //var stream = await imgs.ToList()[0].OpenReadAsync();
        //    var stream = await imgs.OpenReadAsync();
        //    MyImage = ImageSource.FromStream(() => stream);

        //    var imagesList = new List<byte[]>();

        //    //foreach (var img in imgs)
        //    //{
        //    //    var path = img.FullPath;
        //    //    var format = img.ContentType;
        //    //    var imgFormat = (format == "jpeg".ToLower()) ? ImageFormat.Jpeg : ImageFormat.Png;
        //    //    var image = System.Drawing.Image.FromFile(path);

        //    //    var ms = new MemoryStream();

        //    //    var imgSaveFormat = (imgFormat.ToString().ToLower() == "jpeg")
        //    //        ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png;

        //    //    image.Save(ms, imgSaveFormat);
        //    //    var bytes = ms.ToArray();

        //    //    imagesList.Add(bytes);
        //    //}

        //    var imgPath = imgs.FullPath;
        //    var imgContentType = imgs.ContentType;
        //    var imgFormat = (imgContentType == "jpeg".ToLower()) ? ImageFormat.Jpeg : ImageFormat.Png;
        //    var image = System.Drawing.Image.FromFile(imgPath);

        //    var ms = new MemoryStream();

        //    var imgSaveFormat = (imgFormat.ToString().ToLower() == "jpeg")
        //        ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png;

        //    image.Save(ms, imgSaveFormat);
        //    var imgBytes = ms.ToArray();


        //    //var imagesBytesToUpload = imagesList.ToArray();

        //    IsBusy = false;
        //}
    }
}

