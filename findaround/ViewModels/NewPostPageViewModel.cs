using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;
using findaround.Helpers;
using findaroundShared.Helpers;
using findaroundShared.Models;

namespace findaround.ViewModels
{
	public partial class NewPostPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;
        readonly IGeolocation _geolocation;

        List<CategoryDisplayModel> categories;
        public List<CategoryDisplayModel> Categories { get => categories; set => SetProperty(ref categories, value); }

        CategoryDisplayModel selectedCategory;
        public CategoryDisplayModel SelectedCategory { get => selectedCategory;
            set => SetProperty(ref selectedCategory, value); }

        string postTitle;
        public string PostTitle { get => postTitle; set => SetProperty(ref postTitle, value); }

        string description;
        public string Description { get => description; set => SetProperty(ref description, value); }

        bool entriesAvailable;
        public bool EntriesAvailable { get => entriesAvailable; set => SetProperty(ref entriesAvailable, value); }

        ImageSource myImage;
        public ImageSource MyImage { get => myImage; set => SetProperty(ref myImage, value); }

		public NewPostPageViewModel(IPostService postService, IGeolocation geolocation)
		{
			_postService = postService;
            _geolocation = geolocation;

			Title = "Add your post";

            PostTitle = string.Empty;
            Description = string.Empty;

            Categories = new List<CategoryDisplayModel>();

            foreach (PostCategory category in Enum.GetValues(typeof(PostCategory)))
            {
                Categories.Add(new CategoryDisplayModel()
                {
                    Category = category,
                    Name = category.ToString(),
                    Image = category.ToString().ToLower() + "_image.png"
                });
            }
        }

        [RelayCommand]
        void Appearing()
        {
            IsBusy = true;
            EntriesAvailable = false;

            Categories.Clear();

            foreach (PostCategory category in Enum.GetValues(typeof(PostCategory)))
            {
                Categories.Add(new CategoryDisplayModel()
                {
                    Category = category,
                    Name = category.ToString(),
                    Image = category.ToString().ToLower() + "_image.png"
                });
            }

            PostTitle = string.Empty;
            Description = string.Empty;

            SelectedCategory = Categories[0];

            EntriesAvailable = true;
            IsBusy = false;
        }

        [RelayCommand]
        async Task AddPost()
        {
            IsBusy = true;
            EntriesAvailable = false;

            if (string.IsNullOrWhiteSpace(postTitle))
            {
                await Shell.Current.DisplayAlert("Error", "Post must have a title", "OK");
                EntriesAvailable = true;
                IsBusy = false;
            }
            else
            {
                if (SelectedCategory is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Please select a category", "OK");
                    EntriesAvailable = true;
                }
                else
                {
                    var location = await LocationHelpers.GetCurrentLocation(_geolocation);

                    if (location is null)
                    {
                        await Shell.Current.DisplayAlert("Error", "Cannot get location", "OK");
                        EntriesAvailable = true;
                        IsBusy = false;
                    }
                    else
                    {
                        var post = new Post()
                        {
                            AuthorId = UserHelpers.CurrentUser.Id,
                            AuthorName = UserHelpers.CurrentUser.Login,
                            Title = PostTitle,
                            Description = Description,
                            Status = PostStatus.Active,
                            Category = SelectedCategory.Category,
                            Location = new PostLocation
                            {
                                Latitude = location.Longitude,
                                Longitude = location.Latitude
                            },
                            DistanceFromUser = 0.00,
                            Images = new List<PostImage>(),
                            HasImages = false
                    };

                        var isAdded = await _postService.AddPost(post);

                        if (!isAdded)
                        {
                            await Shell.Current.DisplayAlert("Cannot add post", "Something went wrong", "OK");
                            EntriesAvailable = true;
                            IsBusy = false;
                        }
                        else
                        {
                            IsBusy = false;
                            EntriesAvailable = true;
                            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                        }
                    }
                }
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }

        [RelayCommand]
        async Task TryAddMedia()
        {
            await Shell.Current.DisplayAlert("Not supported yet", "This feature will be available soon", "OK");
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

