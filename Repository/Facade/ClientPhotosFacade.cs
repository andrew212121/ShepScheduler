using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Repository.Facade
{
	public class ClientPhotosFacade
	{
		public static List<string> GetImagesForClient(ClientBase client)
		{
			List<string> images = new List<string>();
			string folderPath = GetClientImagesFolder(client);
			if(Directory.Exists(folderPath))
			{ 
				var files = Directory.GetFiles(folderPath);
				foreach (var card in files)
				{
					images.Add(card);
				}
			}
			return images;
		}

		public static List<Photo> AddImagesForClient(ClientBase client, string[] images)
		{
			var photos = new List<Photo>();
			string imagePath = GetClientImagesFolder(client);
			if(Directory.Exists(imagePath))
			{ 
				foreach (var image in images)
				{
					string fileName = image.Remove(0, image.LastIndexOf('\\') + 1);
					string destPath = imagePath + "\\" + fileName;
					File.Copy(image, destPath);
					photos.Add(new Photo{
						Name = fileName,
						Path = destPath
					});
				}
			}
			return photos;
		}

		private static string GetClientImagesFolder(ClientBase client)
		{
			string path = Consts.ClientPhotosPath+client.Id;
			return path;
		}

		public static void CreateFolderForPhotos(ClientBase client)
		{
			string path = GetClientImagesFolder(client);
			Directory.CreateDirectory(path);
		}

		public static void DeleteFolderForPhotos(ClientBase client)
		{
			string path = GetClientImagesFolder(client);
			if(Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}
		}
	}
}
